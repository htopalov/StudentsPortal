import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
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
  isNewStudent = true;
  pageHeader = '';
  displayProfileImageUrl = '';

  @ViewChild('studentDetailsForm') studentDetailsForm?: NgForm;

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

          if(this.studentId.toLowerCase() == 'Add'.toLowerCase()){
            this.isNewStudent = true;
            this.pageHeader = 'Add new student';
            this.setImage();
          } else {
            this.isNewStudent = false;
            this.pageHeader = 'Student Details';
            this.studentService.getStudent(this.studentId)
            .subscribe(
              (successResponse) => {
                this.student = successResponse;
                this.setImage();
              },
              (errorResponse) => {
                this.setImage();
              }
            );
          }

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
    if(this.studentDetailsForm?.form.valid){
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
  }

  onDelete(): void {
    this.studentService.deleteStudent(this.student.id)
    .subscribe((successResponse)=>{
      this.snackBarNotifier.open('Student deleted', undefined, {
        duration: 2000
      });
      setTimeout(()=>{
        this.router.navigateByUrl('/');
      },2000);
    },
    (errorResponse)=>{
      console.log(errorResponse);
    });
  }

  onAdd(): void {
    if(this.studentDetailsForm?.form.valid){
      this.studentService.createStudent(this.student)
      .subscribe(
        (successResponse)=>{
          this.snackBarNotifier.open(`Student ${successResponse.firstName} ${successResponse.lastName} added`, undefined, {
            duration: 2000
          });

          setTimeout(()=>{
            this.router.navigateByUrl('/');
          },2000);
      },
      (errorResponse)=>{
        console.log(errorResponse);
      });
    }
  }

  uploadImage(event:any): void {
    if(this.studentId){
     const file: File = event.target.files[0];
     this.studentService.uploadImage(this.student.id, file)
     .subscribe(
       (successResponse)=> {
         this.student.profileImageUrl = successResponse;
         this.setImage();
         this.snackBarNotifier.open('Profile image has been set', undefined, {
           duration: 2000
         });
     },
     (errorResponse)=>{

     });
    }
  }

  private setImage(): void {
    if(this.student.profileImageUrl){
      this.displayProfileImageUrl = this.studentService.getImagePath(this.student.profileImageUrl);
    } else {
      this.displayProfileImageUrl = '/assets/default-profile.png';
    }
  }
}
