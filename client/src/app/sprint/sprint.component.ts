import { Component, OnInit } from '@angular/core';
import { Sprint } from '../shared/model/sprint';
import { HubConnection } from "@aspnet/signalr-client/dist/src";
import { ActivatedRoute } from "@angular/router";
import swal from 'sweetalert2';
import { ConfigFile } from '../shared/config';
import { ProductBacklog } from '../shared/model/productBacklog';

@Component({
  selector: 'app-sprint',
  templateUrl: './sprint.component.html',
  styleUrls: ['./sprint.component.css']
})
export class SprintComponent implements OnInit {
  //variable declaration
  userId: number;
  projectId: number;
  sprints: Array<Sprint>;
  backlogs: Array<ProductBacklog>;
  connection: HubConnection;
  newsprint = new Sprint(null,'','',null,null,0);
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    //getting member Id from session
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);

    //getting project id from route
    this.route.params.subscribe((param) => this.projectId = +param['id']);

    //register connection methods and get all the sprints according to the project id.
    this.connectBacklogHub();
  }
  //this is for connection to hub socket
  connectBacklogHub() {
    this.connection = new HubConnection(ConfigFile.SprintUrls.connection);

    //register getSprints method and get returned sprints from backend.
    this.connection.on("getSprints", data => {
      this.sprints = data
    });

    //register getBacklogs and get returned backlogs from backend.
    this.connection.on("getBacklogs", data => {
      this.backlogs = data;
      //sort the backlogs according to user story priority
      this.backlogs.sort(function (a, b) {
        return a.priority - b.priority;
      });
    });

    //register postSprints Method and push sprint to the sprints.
    this.connection.on("postSprints", data => {
      swal('Sprint Added', '', 'success');
      this.sprints.push(data);
    });

    //establish the connection and get sprints and backlogs specific to projectId
    this.connection.start()
                    .then(() => {
                      //updates the connection id for the user/
                      this.connection.invoke("SetConnectionId", this.userId); //member Id

                      //get all the sprints for a particular projectID
                      this.connection.invoke("GetSprints", this.projectId);

                      //get all the backlogs
                      this.connection.invoke("GetAllBacklogs",this.projectId);
                    });
  }

  //compare whether story exist in sprint or not
  compareStory(sprintId, inSprintNo) {
    if (sprintId == inSprintNo) {
      return true;          //sprint are available for that particular sprint.
    }
    else {
      return false;         //sprint are not available for particular sprint.
    }
  }

  //assign task to particular members
  updateStoryInSprint($event, sprintNo: number) {
    let storyData: any = $event.dragData;
    //invoke method for updating story in sprint.
    this.connection.invoke("UpdateStoryInSprint", storyData, sprintNo, this.projectId);
  }


  //it will add sprint in the sprint container
  onSaveSprint() {
    this.newsprint.projectId = this.projectId;
    this.newsprint.actualEndDate = new Date(ConfigFile.ActualEndDate);
    //invoke Add Sprint method of Backend
    this.connection.invoke("AddSprint",this.newsprint);
  }
} 