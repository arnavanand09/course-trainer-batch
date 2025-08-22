// src/app/app.routes.ts
import { Routes } from '@angular/router';
import { CourseComponent } from './component/course/course.component';
import { TrainerComponent } from './component/trainer/trainer.component';
import { BatchComponent } from './component/batch/batch.component';

export const routes: Routes = [
  { path: '', redirectTo: 'courses', pathMatch: 'full' },
  { path: 'courses', component: CourseComponent },
  { path: 'trainers', component: TrainerComponent },
  { path: 'batches', component: BatchComponent } // new route for batches
];