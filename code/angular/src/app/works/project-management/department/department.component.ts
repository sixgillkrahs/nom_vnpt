import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { ActiveIndex } from 'src/app/shared/ProjectManager/enums/activeIndex.enum';
import { LocalizationService } from '@ctincsp/ng.core';


@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss'],
  providers: [DialogService]
})
export class DepartmentComponent implements OnInit {

  departments: any[] = [];

  cities: any[];

  selectedCity: any;

  selectedDepartment: any[] = [];

  checked: boolean = true;

  displayDepartment: boolean = false;
  displayAddDepartment: boolean = false;

  departmentDetailData: any;

  constructor(
    private localizationService: LocalizationService
  ) { }

  ngOnInit() {
     this.departments = [
      {
        code: "Csoft",
        name: "Trung tâm phần mềm",
        block: "center",
        status: true
      },
      {
        code: "CTel",
        name: "Trung tâm phần cứng",
        block: "center",
        status: true
      }
     ];

     this.cities = [
      { name: 'New York', code: 'NY' },
      { name: 'Rome', code: 'RM' },
      { name: 'London', code: 'LDN' },
      { name: 'Istanbul', code: 'IST' },
      { name: 'Paris', code: 'PRS' }
    ];

    this.departments.forEach(d => {
      if(d.status){
        this.selectedDepartment.push(d)
      }
    });

  }

  showAddDepartment(){
    this.displayAddDepartment = true;
  }

  cancelModal(event){
    this.displayAddDepartment = false;
  }


  onView(department:any){
    this.displayDepartment = true;
    const data = {
      header: this.localizationService.instant('Work::Pm:Project.Detail'),
      active: ActiveIndex.Detail,
      id: "badsd-tjrnf4-ekejhnr"
    };
    this.departmentDetailData = data;
  }
  closeDepartmentDetail(event){
    if(event){
      this.displayDepartment = false;
      // this.loadData();
    }else{
      this.displayDepartment = false;
    }
  }

}
