import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-modal-confirm-update',
  imports: [],
  templateUrl: './modal-confirm-update.html',
  styleUrl: './modal-confirm-update.css',
})
export class ModalConfirmUpdate {

  constructor(public dialogRef: MatDialogRef<ModalConfirmUpdate>) {}


    fechar(){
    this.dialogRef.close(false);
  }

  btnSim(){
    this.dialogRef.close(true);
  }
}
