import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { HubConnection } from "@aspnet/signalr-client/dist/src";
import { ReleasePlan } from '../shared/model/release-plan';
import swal from 'sweetalert2';
import { ConfigFile } from '../shared/config';

@Component({
  selector: 'app-release-plan',
  templateUrl: './release-plan.component.html',
  styleUrls: ['./release-plan.component.css']
})
export class ReleasePlanComponent implements OnInit {
  listTeamOne: Array<string> = [];
  description: string = "";
  startDate: string = "";
  releaseDate: string = "";
  projectId:any;
  release:any[];
  releasePlan: ReleasePlan = new ReleasePlan();
  sprints:any[];
  data: any;
  connection:HubConnection;
  errorMsg: string;
  userId:number;

  constructor(private router:Router,private route:ActivatedRoute) { }

  //Method for recieving a data from backend 
  connectReleasePlanHub() {
    var session = sessionStorage.getItem("id");//this is to fetch the user id
    this.userId = parseInt(session);
    this.connection = new HubConnection(ConfigFile.ReleasePlanUrls.connection);
    this.connection.on("whenAdded", data => { swal('ADDED','','success' );});
    this.connection.on("getreleaseplans", data => {this.release = data });
    this.connection.on("getsprints", sprint =>{this.sprints=sprint});
    this.connection.start().then(() => {
    this.connection.invoke("SetConnectionId",this.userId);
    this.connection.invoke("GetReleasePlans",this.projectId)
                   .then(()=>this.connection.invoke("GetAllSprints",this.projectId));
    })
    .catch(err=>{                        
      this.errorMsg=err;
      this.router.navigate(['/app-error/'+this.errorMsg]);
    });
  }

  ngOnInit() {
    //getting project id from route
    this.route.params.subscribe((param) => this.projectId = +param['id']);
    this.connectReleasePlanHub();
  }

  //this method is to go back on previous page
  navigateNewRelease(){
    this.router.navigateByUrl(ConfigFile.ReleasePlanUrls.navigateNewRrelease);
  }

  //method for updating a release in sprint
  updateReleaseInSprint($event,releaseId:number){
    let sprintData: any = $event.dragData;
    this.connection.invoke("UpdateReleaseInSprint",sprintData,releaseId).catch(err=>{                        
      this.errorMsg=err;
      this.router.navigate(['/app-error/'+this.errorMsg]);
    });
  }

  //Method for comparing a release plan
  compareStory(releasePlanId,inreleasePlanId)
  {
     if(releasePlanId == inreleasePlanId) return true;
     else return false;
  }

   //this method adds a new release
   addingNewRelease() {
    if ((this.releasePlan.releaseName == undefined)
      || (this.releasePlan.description == undefined)
      || (this.releasePlan.startDate == undefined)
      || (this.releasePlan.releaseDate == undefined)) {
      swal('PLEASE FILL ALL DETAILS', '', 'error');
    }
    else {
      
      var date1 = this.releasePlan.releaseDate.split("-");
      var date2 = this.releasePlan.startDate.split("-");
      if (date2[0] <= date1[0])//year
      {
        if (date2[1] <= date1[1]) // month
        {
          if (date2[2] <= date1[2])//date
           {
            this.releasePlan.projectId = this.projectId;
            this.releasePlan.actualReleaseDate=new Date(ConfigFile.ActualEndDate);
            this.connection.invoke("AddRelease",this.releasePlan)
                           .then(() =>{ swal('Added Successfully','','success');this.connection.invoke("GetReleasePlans",this.projectId)})
                           .catch(err=>{                        
                            this.errorMsg=err;
                            this.router.navigate(['/app-error/'+this.errorMsg]);
                          });
          }
          else {
            swal('enter valid date', '', 'error') //alert for a date
          }
        }
        else {
          swal('enter valid months', '', 'error') //alert for a month
        }
      }
      else {
        swal('enter valid year', '', 'error')  //alert for a year
      } 
    } 
  }
}