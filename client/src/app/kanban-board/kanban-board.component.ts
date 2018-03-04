import { Component, OnInit } from '@angular/core';
import { KanbanService } from "../shared/services/kanban.service";
import { TaskBackLog } from "../shared/model/TaskBacklog";
import { Sprint } from "../shared/model/Sprint";
import { ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'kanban',
  templateUrl: './kanban-board.component.html',
  styleUrls: ['./kanban-board.component.css']
})
export class KanbanBoardComponent implements OnInit {

  //local variable used in backend
  isDataAvailable = false;
  projectData:Array<any> = [];
  releaseData:Array<any> = [];
  sprintData:Array<any> = [];
  taskList:Array<TaskBackLog> = [];
  sprintList:Array<Sprint> =[];
  releaseList:Array<any> = [];
  showList:Array<any> =[];
  projectId:number;
  sprintItem:string;
  releaseItem:string;

  constructor(private kanbanService : KanbanService, private route : ActivatedRoute) { }

  ngOnInit() {

    // getting sprint id from route 
    this.route.params.subscribe((param) =>{this.projectId = +param['id']});
    //get project related data for a project
    this.kanbanService.getProjectDetails(this.projectId)
    .subscribe(projectData=>{ this.isDataAvailable= true;
                              this.projectData = projectData;
                              //call initialize method
                              this.initialize();
                              //call fulldetail for first landing view
                              this.fulldetail();
                              });

  }
  
  //this method is to fill the list according to the project sprint and task and release plan
  initialize()
  {
   this.projectData.forEach((p,i) => p.sprints.forEach((q,i)=> this.sprintData.push(q["sprintName"])));
   this.projectData.forEach((p,i) => this.releaseData.push(p["releaseName"]));
  }

  //this method is to assign the value to the main list so that it can be fill according to release plan status
  fulldetail(){
    this.showList =[];
  //  this.projectData.forEach((p,i) => this.showList.push(p));
  this.showList = this.projectData;
  }
  
  //this method is to assign the value to the main list so that it can be fill according to sprint status
  releaseDetail(){
    this.showList =[];
    this.projectData.forEach((p,i) => {
                                        if(p["releaseName"] == this.releaseItem)
                                          {
                                            p.sprints.forEach((q,i) => this.showList.push(q));
                                          }});
  }

  //this method is to assign the value to the main list so that it can be fill according to task status
  sprintDetail(){
    this.showList =[];
    this.projectData.forEach((p,i) =>p.sprints.forEach((q,i) => {
                                      if(q["sprintName"] == this.sprintItem)
                                        {
                                          q.tasks.forEach((r,i) => this.showList.push(r));
                                        }
    }));
  }



}
