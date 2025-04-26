import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-work-template',
  templateUrl: './work-template.component.html',
  styleUrls: ['./work-template.component.scss']
})
export class WorkTemplateComponent implements OnInit {

  process: any[] = [];

  cities: any[];

  selectedProcess: any[] = [];

  startDate: any;
  endDate: any;

  constructor() { }

  ngOnInit() {

    this.process = [
      {
        name: "Mặc định",
        classify: "HR",
        defailt: true,
        status: true
      },
      {
        name: "Tuyển dụng",
        classify: "HR",
        defailt: false,
        status: true
      },
      {
        name: "Mua sắm",
        classify: "IR",
        defailt: false,
        status: true
      },
     ];

     this.cities = [
      { code: 'HR' },
      { code: 'IR' },
    ];

    this.process.forEach(p => {
      if(p.defailt){
        this.selectedProcess.push(p)
      }
    });

  }

}
