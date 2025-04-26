import { Component, Input, OnInit } from '@angular/core';
import { LocalizationService } from '@ctincsp/ng.core';
import { ActiveIndex } from 'src/app/shared/ProjectManager/enums/activeIndex.enum';
import { ProjectDetailData } from 'src/app/shared/ProjectManager/models/project-manager.model';

@Component({
  selector: 'app-department-detail',
  templateUrl: './department-detail.component.html',
})
export class DepartmentDetailComponent implements OnInit {

  projects: any[] = [];

  constructor(
    private localizationService: LocalizationService )
  {}

  ngOnInit() {
    this.getProjects();
  }

  getProjects(){
    this.projects = [
      {
        code: "dfsnf1",
        name: "Trung tâm phần mềm",
        department: "Csoft",
        state: "Đấu thầu",
        progress: 70,
        status: true
      },
      {
        code: "dfsnf2",
        name: "Trung tâm phần cứng",
        department: "CTel",
        state: "Đấu thầu",
        progress: 40,
        status: true
      }
     ];

  }

}
