import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import { ConfigFile } from "../config";

@Injectable()
export class KanbanService {

  constructor(private http : Http) { }

  //header
  token= sessionStorage.getItem("token");
  headers = new Headers({'Content-Type':'application/json','Authorization':'Bearer '+this.token});
  options = new RequestOptions({ headers: this.headers});


  getProjectDetails(projectId:number) {

    //this method is to bring all the details related to the particular project
    return this.http.get(ConfigFile.KanBanUrls.getProjectData+projectId)
                    .map(Response=>Response.json());
  }

}
