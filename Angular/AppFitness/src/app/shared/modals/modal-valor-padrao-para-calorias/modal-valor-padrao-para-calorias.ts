import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-modal-valor-padrao-para-calorias',
  imports: [],
  templateUrl: './modal-valor-padrao-para-calorias.html',
  styleUrl: './modal-valor-padrao-para-calorias.css',
})
export class ModalValorPadraoParaCalorias {

  constructor(public dialogRef: MatDialogRef<ModalValorPadraoParaCalorias>) {}

  btnOk(){
    this.dialogRef.close(true);
  }
}
