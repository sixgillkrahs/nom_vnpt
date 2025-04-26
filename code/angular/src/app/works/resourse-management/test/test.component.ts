import { Component, OnInit } from '@angular/core';
import { ListService, LocalizationService, PagedResultDto } from '@ctincsp/ng.core';
import {DocumentService } from '@proxy/resourse-management';
import { DocumentGetListInput,DocumentDto } from '@proxy/resourse-management/dtos';
import { ConfirmationService } from 'primeng/api';
// import { DocumentData } from 'src/app/shared/ProjectManager/models/project-manager.model';


@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.scss'],
  providers: [ListService]
})


export class TestComponent implements OnInit {
  document : DocumentDto[] = [];
  total: number ;
  display : boolean = false;
  displaycreate : boolean = false;
  actionItems: any[] = [];
  // resourseData:ResourseData ;
  filter: DocumentGetListInput;
  isLoading: boolean = false;
  imageBytes: Uint8Array; 

  //lifecycle
  ngOnInit(): void {
    this.getresourse();
  }
  //constructor
  constructor(
    
    private documentService: DocumentService,
    private localizationService : LocalizationService,
    private confirmationService: ConfirmationService,
  ) {
    
  }
  //get resourse from api
  getresourse(){
    this.filter = {
      skipCount: 0,
      maxResultCount: 10,
    } as DocumentGetListInput;
    this.documentService.getAllByInput(this.filter).subscribe({
      next: (document) => {
        this.isLoading = true;
        this.document = document.items;
        this.total = document.totalCount;
        console.log(document)
      },
      error: (err) => {
        this.isLoading = false;
      }
    })
  }



  onView(project: any) {
    this.display = true;
    const data = {
      header: this.localizationService.instant('Work::Pm:Project.Detail'),
      id: project.id,
      params : project
    };
    // this.resourseData = data;
    // console.log(this.resourseData)
  }
  //show modal create or edit
  showModal(resourse?:any){
    this.displaycreate = true;

    // let data: ResourseData  = {
    //   header: this.localizationService.instant('New Document'),
    // }
    

    if(resourse){
      // data.id = resourse.id;
      // data.param = resourse;
    }
    

    // this.resourseData = data;
   
  }

  close(){
    this.display = false;
    this.displaycreate = false;
  }

 //dropdown items
  // dropdownItemsButton(resourse) {
  //   this.actionItems = [
  //     {
  //       label: this.localizationService.instant("Work::Edit"),
  //       icon: 'pi pi-pencil',
  //       command: () => { this.showModal(resourse) }
  //     },
  //     { separator: true },
  //     {
  //       label: this.localizationService.instant("Work::Delete"),
  //       icon: "pi pi-trash",
  //       command: () => { this.confirmDelete(resourse); },
  //     },
  //   ]
  // }
  //comfirm before delete
  // confirmDelete(resourse: ResoureManagementDto) {
  //   this.confirmationService.confirm({
  //     message: this.localizationService.instant('Work::Message:Confirm.Delete', resourse.name),
  //     accept: () => {
  //       this.deleteProjectRole(resourse);
  //     },
  //   });
  // }
  //delete resourse
  // deleteProjectRole(resourse: ResoureManagementDto) {
  //   this..delete(resourse.id).subscribe({
  //     next: (res) => {
  //       this.getresourse();
  //       console.log(res)
  //     }
  //   }) 
  // }

  closeresourse(event) {
    if (event) {
      this.getresourse();
      this.displaycreate = false;
    } else {
      this.displaycreate = false;
    }
  }


}