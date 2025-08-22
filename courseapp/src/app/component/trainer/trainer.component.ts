// src/app/components/trainer/trainer.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TrainerService } from '../../services/trainer.service';
import { Trainer } from '../../models/trainer';

@Component({
  selector: 'app-trainer',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './trainer.component.html'
})
export class TrainerComponent implements OnInit {

  trainers: Trainer[] = [];
  formTrainer: Trainer = this.getEmptyTrainer(); // single object for add/edit
  editingTrainer: Trainer | null = null;
  errorMessage: string = '';

  constructor(private trainerService: TrainerService) {}

  ngOnInit(): void {
    this.loadTrainers();
  }

  // Load all trainers
  loadTrainers(): void {
    this.trainerService.getTrainers().subscribe({
      next: data => this.trainers = data,
      error: () => this.errorMessage = 'Failed to load trainers'
    });
  }

  // Prepare empty trainer object
  private getEmptyTrainer(): Trainer {
    return {
      trainerId: 0,
      trainerName: '',
      expertise: '',
      email: '',
      phone: '',
      isActive: true,
      createdBy: 1,
      createdOn: new Date().toISOString(),
      modifiedBy: undefined,
      modifiedOn: undefined,
      batches: []
    };
  }

  // Save (Add or Update)
  saveTrainer(): void {
    if (this.editingTrainer) {
      this.trainerService.updateTrainer(this.editingTrainer.trainerId, this.formTrainer)
        .subscribe({
          next: () => {
            this.loadTrainers();
            this.cancelEdit();
          },
          error: () => this.errorMessage = 'Failed to update trainer'
        });
    } else {
      this.trainerService.addTrainer(this.formTrainer)
        .subscribe({
          next: () => {
            this.loadTrainers();
            this.formTrainer = this.getEmptyTrainer();
          },
          error: () => this.errorMessage = 'Failed to add trainer'
        });
    }
  }

  // Start editing
  editTrainer(trainer: Trainer): void {
    this.editingTrainer = trainer;
    this.formTrainer = { ...trainer }; // copy for editing
  }

  // Cancel edit
  cancelEdit(): void {
    this.editingTrainer = null;
    this.formTrainer = this.getEmptyTrainer();
  }

  // Delete trainer
  deleteTrainer(id: number): void {
    if (confirm('Are you sure you want to delete this trainer?')) {
      this.trainerService.deleteTrainer(id).subscribe({
        next: () => this.loadTrainers(),
        error: () => this.errorMessage = 'Failed to delete trainer'
      });
    }
  }
}
