import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../models/api-models/student.model';
import { UpdateStudent } from '../models/api-models/update-student.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private baseApiUrl = 'https://localhost:7109';
  constructor(private httpClient: HttpClient) { }

  getAllStudents(): Observable<Student[]>{
    return this.httpClient.get<Student[]>(this.baseApiUrl + '/api/student/all');
  }

  getStudent(studentId:string):Observable<Student> {
    return this.httpClient.get<Student>(this.baseApiUrl + '/api/student/' + studentId);
  }

  updateStudent(studentId:string, updateStudent: Student): Observable<Student>{
    const updateStudentRequest: UpdateStudent = {
      firstName: updateStudent.firstName,
      lastName: updateStudent.lastName,
      birthDate: updateStudent.birthDate,
      email: updateStudent.email,
      phone: updateStudent.phone,
      genderId: updateStudent.genderId,
      physicalAddress: updateStudent.address.physicalAddress,
      postalAddress: updateStudent.address.postalAddress
    }

    return this.httpClient.put<Student>(this.baseApiUrl + '/api/student/' + studentId, updateStudentRequest);
  }
}
