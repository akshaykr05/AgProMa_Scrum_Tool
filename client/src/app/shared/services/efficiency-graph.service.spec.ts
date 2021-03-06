import { TestBed, async, inject } from '@angular/core/testing';
import {
  HttpModule,
  Http, RequestMethod,
  Response,
  ResponseOptions,
  XHRBackend
} from '@angular/http';
import { RouterTestingModule } from '@angular/router/testing';
import { MockBackend } from '@angular/http/testing';
import { FormsModule } from '@angular/forms';
import { EfficiencyGraphService } from './efficiency-graph.service';
import { TaskBackLog } from "../model/TaskBackLog";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
const mockResponse =                              //Response being mocked for testing
  [
    { "TaskId": 29, "SprintId": 33, "TaskName": "Testing", "PersonId": 37 },
    { "TaskId": 30, "SprintId": 34, "TaskName": "Testing", "PersonId": 38 },
    { "TaskId": 31, "SprintId": 35, "TaskName": "Testing", "PersonId": 39 },
    { "TaskId": 32, "SprintId": 36, "TaskName": "Testing", "PersonId": 40 }
  ];
describe('EfficiencyGraphService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({                              /* creates a module that overrides the actual dependencies with testing dependencies*/
      imports: [HttpModule, RouterTestingModule, FormsModule],    //imports being declared before setting the testing environment
      providers: [
        EfficiencyGraphService,                                      //provider tells the service on which testing is being performed
        { provide: XHRBackend, useClass: MockBackend },
      ]
    });
  });                                                    //end of first before each
  describe('efficiency', () => {              //start of second describe
    let service: EfficiencyGraphService;
    beforeEach(inject([Http, XHRBackend], (http: Http, back: MockBackend) => {    /* injecting service and backend dependencies*/
      service = new EfficiencyGraphService(http);
    }));
    it('can instantiate service when inject service',       //First test case begins
      inject([EfficiencyGraphService], (service: EfficiencyGraphService) => {
        expect(service instanceof EfficiencyGraphService).toBe(true);
      }));                                                      //First test case ends
    it('can instantiate service with "new"', inject([Http], (http: Http) => {      //Second test case begins
      expect(http).not.toBeNull('http should be provided');
      let service = new EfficiencyGraphService(http);
      expect(service instanceof EfficiencyGraphService).toBe(true, 'new service should be ok');
    }));                                                                            //Second test case ends
    it('can provide the mockBackend as XHRBackend',                   //Third test case begins
      inject([XHRBackend], (backend: MockBackend) => {
        expect(backend).not.toBeNull('backend should be provided');
      }));
    //Third test case ends
    it('getEfficiencyDetail',             //Fourth test case Begins
      inject([EfficiencyGraphService, XHRBackend], (EfficiencyGraphService, mockBackend) => {      //injecting the service and backend dependencies
        mockBackend.connections.subscribe((connection) => {          /* setting up connections to Http whenever someone subcribes 
                                                             to an http call */
          connection.mockRespond(new Response(new ResponseOptions({
            body: JSON.stringify(mockResponse)
          })));
        });
        EfficiencyGraphService.getEfficiencyDetail(1).subscribe((data) => {
          expect(data.length).toBe(4);
          expect(data[0].TaskId).toEqual(29);
          expect(data[1].SprintId).toEqual(34);
          expect(data[2].TaskName).toEqual("Testing");
          expect(data[3].PersonId).toEqual(40);
        });
      }));
  })
});