import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Gender } from 'src/app/models/ui-models/gender.model';
import { Student } from 'src/app/models/ui-models/student.model';
import { GenderService } from 'src/app/services/gender.service';
import { StudentService } from '../student.service';

@Component({
  selector: 'app-student-details',
  templateUrl: './student-details.component.html',
  styleUrls: ['./student-details.component.css']
})
export class StudentDetailsComponent implements OnInit {
  studentId: string | null | undefined;
  student: Student = {
    id: '',
    firstName: '',
    lastName: '',
    birthDate: '',
    email: '',
    phone: '',
    genderId: '',
    profileImageUrl: '',
    gender: {
      id: '',
      description: '',
    },
    addressId: '',
    address: {
      id: '',
      physicalAddress: '',
      postalAddress: ''
    }
  };

  genderList: Gender[] = [];

  constructor(private readonly studentService: StudentService,
    private readonly route: ActivatedRoute,
    private readonly genderService: GenderService,
    private readonly snackBarNotifier: MatSnackBar,
    private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (params)=> {
        this.studentId = params.get('id');

        if(this.studentId){
          this.studentService.getStudent(this.studentId)
          .subscribe(
            (successResponse) => {
              this.student = successResponse;
            }
          );
          this.genderService.getGenderList()
          .subscribe(
            (successResponse) => {
              this.genderList = successResponse;
            }
          );
        }
      }
    );
  }

  onUpdate(): void {
    this.studentService.updateStudent(this.student.id, this.student)
    .subscribe((successResponse)=>{
      this.snackBarNotifier.open(`Student ${successResponse.firstName} ${successResponse.lastName} updated`, undefined, {
        duration: 2000
      });
    },
    (errorResponse)=>{
      console.log(errorResponse);
    });
  }

  onDelete(): void {
    this.studentService.deleteStudent(this.student.id)
    .subscribe((successResponse)=>{
      this.snackBarNotifier.open('Student deleted', undefined, {
        duration: 2000
      });
      setTimeout(()=>{
        this.router.navigateByUrl('/api/student/all');
      },2000);
    },
    (errorResponse)=>{
      console.log(errorResponse);
    });
  }
}
