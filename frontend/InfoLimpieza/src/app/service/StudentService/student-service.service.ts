import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Student } from 'src/app/interface/models';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})
export class StudentServiceService {
  myUrl : string = environment.endpoint;
  myApi : string = '/GetAllStudents';

  constructor(public http: HttpClient) { }

  getAllStudent(): Observable<Array<Student>> {
    console.log(`este es la peticion : ${this.myUrl}${this.myApi}`);
    return this.http.get<Array<Student>>(`${this.myUrl}${this.myApi}`);
  }
}
