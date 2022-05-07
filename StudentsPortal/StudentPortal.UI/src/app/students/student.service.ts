import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AddStudent } from '../models/api-models/add-student.model';
import { Student } from '../models/api-models/student.model';
import { UpdateStudent } from '../models/api-models/update-student.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private baseApiUrl = environment.baseApiUrl;

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

  deleteStudent(studentId:string): Observable<Student>{
    return this.httpClient.delete<Student>(this.baseApiUrl + '/api/student/' + studentId);
  }

  createStudent(createStudent: Student): Observable<Student>{
    const createStudentRequest: AddStudent = {
      firstName: createStudent.firstName,
      lastName: createStudent.lastName,
      birthDate: createStudent.birthDate,
      email: createStudent.email,
      phone: createStudent.phone,
      genderId: createStudent.genderId,
      physicalAddress: createStudent.address.physicalAddress,
      postalAddress: createStudent.address.postalAddress
    };

    return this.httpClient.post<Student>(this.baseApiUrl + '/api/student/add', createStudentRequest);
  }

  uploadImage(studentId: string, file: File): Observable<any>{
    const formData = new FormData();
    formData.append("profileImage", file);
    return this.httpClient.post(this.baseApiUrl + '/api/student/' + studentId + '/upload-image', formData, {responseType: 'text'});
  }

  getImagePath(relativePath: string){
    return `${this.baseApiUrl}/${relativePath}`;
  }
}
