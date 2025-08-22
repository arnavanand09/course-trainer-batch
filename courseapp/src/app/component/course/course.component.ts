// src/app/components/course/course.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CourseService } from '../../services/course.service';
import { Course } from '../../models/course';

@Component({
  selector: 'app-course',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {
  courses: Course[] = [];
  newCourse: Course = this.getEmptyCourse();
  editMode = false;

  constructor(private courseService: CourseService) {}

  ngOnInit(): void {
    this.loadCourses();
  }

  loadCourses() {
    this.courseService.getCourses().subscribe(data => {
      this.courses = data;
    });
  }

  addCourse() {
    this.newCourse.createdOn = new Date().toISOString();
    this.newCourse.createdBy = 1; // example userId

    this.courseService.addCourse(this.newCourse).subscribe(() => {
      this.loadCourses();
      this.resetForm();
    });
  }

  editCourse(course: Course) {
    this.newCourse = { ...course };
    this.editMode = true;
  }

  updateCourse() {
    this.newCourse.modifiedOn = new Date().toISOString();
    this.newCourse.modifiedBy = 1; // example userId

    this.courseService.updateCourse(this.newCourse.courseId, this.newCourse)
      .subscribe(() => {
        this.loadCourses();
        this.resetForm();
      });
  }

  deleteCourse(id: number) {
    this.courseService.deleteCourse(id).subscribe(() => {
      this.loadCourses();
    });
  }

  resetForm() {
    this.newCourse = this.getEmptyCourse();
    this.editMode = false;
  }

  private getEmptyCourse(): Course {
    return {
      courseId: 0,
      courseName: '',
      description: '',
      duration: 0,
      createdBy: 1,
      createdOn: new Date().toISOString(),
      isActive: true
    };
  }
}
