import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-department-container',
  templateUrl: './department-container.component.html',
  styleUrls: ['./department-container.component.scss']
})
export class DepartmentContainerComponent implements OnInit {

  @Input() displayDepartment: boolean = false;
  @Input() departmentDetailData: any;
  @Output() closeDepartmentDetail = new EventEmitter();

  constructor() {}

  ngOnInit() {
    console.log("zo");
  }

}
