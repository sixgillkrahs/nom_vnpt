import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-department',
  templateUrl: './add-department.component.html',
  styleUrls: ['./add-department.component.scss']
})
export class AddDepartmentComponent implements OnInit {

  @Input() displayAddDepartment: boolean;
  @Output() closeModal: EventEmitter<any> = new EventEmitter();

  formGroup!: FormGroup;

  constructor(private fb: FormBuilder) { }

  get f() { return this.formGroup.controls; }

  ngOnInit() {
    this.createFormBuild();
  }

  createFormBuild(){
    this.formGroup = this.fb.group({
      code: [null, Validators.required],
      name: [null, Validators.required],
      block: [null, Validators.required]
    })
  }

  save(){

  }


}
