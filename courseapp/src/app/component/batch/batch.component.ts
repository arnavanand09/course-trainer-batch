// src/app/component/batch/batch.component.ts

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BatchService } from '../../services/batch.service';
import { Batch } from '../../models/batch';

@Component({
  selector: 'app-batch',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './batch.component.html',
  styleUrls: ['./batch.component.css']
})
export class BatchComponent implements OnInit {
  batches: Batch[] = [];
  selectedBatch: Batch | null = null;
  currentBatch: Batch = {
    batchId: 0,
    batchName: '',
    startDate: new Date(),
    endDate: new Date(),
    isActive: true,
    courseId: 0,
    createdBy: 1,
    createdOn: new Date()
  };

  constructor(private batchService: BatchService) {}

  ngOnInit(): void {
    this.loadBatches();
  }

loadBatches(): void {
  this.batchService.getBatches().subscribe({
    next: (data) => this.batches = data,
    error: (err) => {
      if (err.status === 404 && err.error?.message === 'No batches found') {
        // Correctly treat as empty
        this.batches = [];
      } else {
        // Only log unexpected errors
        console.error('Failed to load batches', err);
      }
    }
  });
}


  onSelect(batch: Batch): void {
    this.selectedBatch = batch;
    this.currentBatch = { ...batch };
  }

  onAddBatch(): void {
    const batchToAdd = {
      batchName: this.currentBatch.batchName,
      startDate: new Date(this.currentBatch.startDate).toISOString(),
      endDate: new Date(this.currentBatch.endDate).toISOString(),
      isActive: this.currentBatch.isActive,
      courseId: this.currentBatch.courseId,
      trainerId: this.currentBatch.trainerId,
      createdBy: 1,
      createdOn: new Date().toISOString()
    };

    this.batchService.addBatch(batchToAdd as any).subscribe({
      next: () => {
        // Small delay to ensure batch is saved before reloading
        setTimeout(() => {
          this.loadBatches();
          this.resetForm();
        }, 500);
      },
      error: (err) => console.error('Failed to add batch', err)
    });
  }

  onUpdateBatch(): void {
    if (this.selectedBatch) {
      this.batchService.updateBatch(this.selectedBatch.batchId, this.currentBatch).subscribe({
        next: () => {
          this.loadBatches();
          this.resetForm();
        },
        error: (err) => console.error('Failed to update batch', err)
      });
    }
  }

  onDeleteBatch(id: number): void {
    if (confirm('Are you sure you want to delete this batch?')) {
      this.batchService.deleteBatch(id).subscribe({
        next: () => this.loadBatches(),
        error: (err) => console.error('Failed to delete batch', err)
      });
    }
  }

  resetForm(): void {
    this.selectedBatch = null;
    this.currentBatch = {
      batchId: 0,
      batchName: '',
      startDate: new Date(),
      endDate: new Date(),
      isActive: true,
      courseId: 0,
      createdBy: 1,
      createdOn: new Date()
    };
  }
}
