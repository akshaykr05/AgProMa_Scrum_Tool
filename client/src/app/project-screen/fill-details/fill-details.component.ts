import { Component, OnInit } from '@angular/core';
import { ProjectMaster } from '../../shared/model/ProjectMaster';
import { ProjectScreenService } from './../../shared/services/project-screen.service';
import { Router,ActivatedRoute } from '@angular/router';
import { ConfigFile } from './../../shared/config';

@Component({
  selector: 'app-fill-details',
  templateUrl: './fill-details.component.html',
  styleUrls: ['./fill-details.component.css']
})
export class FillDetailsComponent implements OnInit {
  updateId:number;
  condition:string;
  token:boolean;
  projectname:string;
  projectdescription:string;
  technologies:string;
  leaderId:number;
  constructor(private projectscrservice:ProjectScreenService,private route:ActivatedRoute,private router:Router) { }

  ngOnInit() {
    this.route.params.subscribe((param) => {
                                      this.updateId = +param.id;
                                      this.condition = param.id;
    });
    var session = sessionStorage.getItem("id");
    this.leaderId = parseInt(session);
  }

  //this method is to go back on previous page
  backOnPrevious(){
    this.router.navigateByUrl(ConfigFile.FillDetailsUrls.backOnPrevious);
  }

  //this method is used to add new project
  AddProject()
  {
    if(this.projectdescription != null && this.projectname !=null && this.technologies != null)
    {
      let projectitem = new ProjectMaster(this.projectname,this.leaderId,this.projectdescription,this.technologies);
      this.projectscrservice.addNewProject(projectitem)
                            .then(data => this.router.navigate([ConfigFile.FillDetailsUrls.dashboardNavigation]));
    }
    else
    {
      window.alert("please fill all the details, Thank you!!!");
    }
  }

  //method is calling the service method to update project
  updateProject()
  {
    if(this.projectdescription != null && this.projectname !=null && this.technologies != null)
    {
      let projectitem = new ProjectMaster(this.projectname,this.updateId,this.projectdescription,this.technologies);
      this.projectscrservice.updateProject(this.updateId,projectitem)
                            .then(data => this.router.navigate([ConfigFile.FillDetailsUrls.dashboardNavigation]));
    }
    else
    {
      window.alert("please fill all the details, Thank you!!!");
    }
  }
}
