import { Component, OnInit } from '@angular/core';
import { LocalizationService, NotificationService, PagedResultDto } from '@ctincsp/ng.core';
import { DepartmentTeamDto, DepartmentTeamGetListInput } from '@proxy/department-teams/dtos';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { ActiveIndex, DepartmentTeamAction } from 'src/app/shared/DepartmentTeam/enums/activeIndex.enum';
import { DepartmentTeamDetailData } from 'src/app/shared/DepartmentTeam/models/department-team.model';
import { DepartmentTeamService } from '@proxy/department-teams';
import { OrganizationUnitCreateDto, OrganizationUnitDto, OrganizationUnitService, OrganizationUnitLookupDto } from '@ctincsp/ng.identity/proxy';
import { NavigationEnd, Router } from '@angular/router';
import { BreadcrumDefineService } from 'src/app/shared/services/BreadcrumDefine.service';
import { filter } from 'rxjs';

@Component({
  selector: 'app-department-team',
  templateUrl: './department-team.component.html',
  styleUrls: ['./department-team.component.scss'],
  providers: [BreadcrumDefineService]
})
export class DepartmentTeamComponent implements OnInit {
  // Mock data for the tree
  //organizationLookupData: OrganizationUnitLookupDto[] = [];

  items: MenuItem[] | undefined;

  home: MenuItem | undefined;

  organizationLookupData: OrganizationUnitDto[] = [];
  selectedOrganization: OrganizationUnitLookupDto | null = null;
  limitedLevel: number = 1;
  isLoading: boolean = false;

  departmentTeamAction = DepartmentTeamAction;

  departmentTeam: DepartmentTeamDto[] = [];
  departmentTeamFilter: DepartmentTeamGetListInput;

  selectedDepartmentTeam: any[] = [];

  startDate: any;
  endDate: any;

  displayDepartmentTeamDetail: boolean = false;
  displayDepartmentTeamContainer: boolean = false;

  departmentTeamDetailData: DepartmentTeamDetailData;
  departmentTeamDetail: DepartmentTeamDetailData;

  keyword: any;
  departments: any[] = [];
  displayDepartmentTeam: boolean = false;

  displayApproval: boolean = false;
  isButtonVisible: boolean = false;
  constructor(
    private localizationService: LocalizationService,
    private departmentTeamService: DepartmentTeamService,
    private confirmationService: ConfirmationService,
    private notificationService: NotificationService,
    private organizationUnitService: OrganizationUnitService,
    private router: Router,
    breadcrum: BreadcrumDefineService
  ) {

    this.router.events.pipe(filter((event) => event instanceof NavigationEnd)).subscribe(event => {

      const root = this.router.routerState.snapshot.root;

      const routeConfig = root.children[0];

      this.items = breadcrum.getBreadcrum(routeConfig.children[0].routeConfig);
    });
  }

  ngOnInit() {
    this.getListClientId();
    this.loadOrganizationLookup();
    this.home = { icon: 'pi pi-home', routerLink: '/' };
  }

  actionItems: any[] = [];
  dropdownItemsButton(team){
    this.actionItems = [
      {
          label: this.localizationService.instant("Work::Edit"),
          icon: 'pi pi-pencil',
          command: () => { this.showModal(team.id)}
      },
      {
        label: this.localizationService.instant("Work::View"),
        icon: 'pi pi-eye',
        command: () => { this.onView(team.id)}
      },
      {
        label: this.localizationService.instant("Approval"),
        icon: 'pi pi-send',
        command: () => { this.onViewApproval(team.id)},
        visible: this.isButtonVisible
      },
      { separator: true},
      {
          label: this.localizationService.instant("Work::Delete"),
          icon: "pi pi-trash",
          command: () => { this.confirmDelete(team); },
      },
    ]
  }

  getListClientId() {
    //this.isLoading = true;
    const filter = this.keyword ? this.keyword : '';
    const departmentID = this.selectedOrganization ? this.selectedOrganization.id : null;
    this.departmentTeamService.getListClientIdByInput({ filter: filter, departmentID: departmentID, maxResultCount: 10 }).subscribe(res => {
      this.departmentTeam = res.items;
    });
    // setTimeout(() => {
    //   // Kết thúc loading khi tải dữ liệu xong
    //   this.isLoading = false;
    // }, 2000);
    //this.isLoading = false;
  }

  onView(id?: any) {
    this.displayDepartmentTeamDetail = true;
    const data = {
      header: this.localizationService.instant('Thông tin chi tiết team'),
      active: ActiveIndex.Detail,
      id: id
    };
    this.departmentTeamDetailData = data;
  }
  onViewApproval(id?: any) {
    this.displayApproval = true;
    const data = {
      header: this.localizationService.instant('Cấu hình phê duyệt'),
      active: ActiveIndex.Detail,
      id: id
    };
    this.departmentTeamDetailData = data;
  }

  showModal(id?: any) {
    //this.isLoading = true;
    this.displayDepartmentTeamContainer = true;
    //this.localizationService.instant('Work::DT:Department.New');
    const data: DepartmentTeamDetailData = {
      header: this.localizationService.instant('Thêm mới team'),
      active: ActiveIndex.Detail,
    };

    if (id) {
      data.id = id;
      data.header = this.localizationService.instant('Cập nhật team');
    }
    this.departmentTeamDetailData = data;
    // setTimeout(() => {
    //   // Kết thúc loading khi tải dữ liệu xong
    //   this.isLoading = false;
    // }, 3000);
  }

  confirmDelete(team: DepartmentTeamDto) {
    this.confirmationService.confirm({
      message: this.localizationService.instant('Work::Message:Confirm.Delete', team.name),
      accept: () => {
        this.delete(team);
      },
    });
  }

  delete(team: DepartmentTeamDto) {
    this.departmentTeamService.delete(team.id).subscribe({
      next: (res) => {
        this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Delete.Ok", team.name));
        this.getListClientId();
      },
      error: (err) => {
        console.error(err);
      }
    })
  }

  onCloseContainer(event: boolean): void {
    // Xử lý khi modal đóng
    if (event) {
      // Load lại trang danh sách hoặc thực hiện các hàm cần thiết
      this.getListClientId();
    }
  }

  closeContainer(event) {
    if (event) {
      this.getListClientId();
      this.displayDepartmentTeamContainer = false;
      this.displayDepartmentTeamDetail = false;
      //
      this.displayApproval = false;
      //
    } else {
      this.displayDepartmentTeamContainer = false;
      this.displayDepartmentTeamDetail = false;
      //
      this.displayApproval = false;
      //
    }
  }
  loadOrganizationLookup(): void {
    // this.organizationUnitService.getLookup(this.limitedLevel).subscribe((data) => {
    //  this.organizationLookupData = data;
    this.organizationUnitService.getList({ maxResultCount: 100 }).subscribe((data: PagedResultDto<OrganizationUnitDto>) => {
      if (data && data.items) {
        this.organizationLookupData = data.items;
      } else {
        console.error('Invalid API response structure');
      }
    });
  }

  onInputChange() {
    console.log('Keyword changed:', this.keyword);
    // Bạn có thể thực hiện các xử lý khác ở đây nếu cần.
  }
  search() {
    this.getListClientId();

  }
  // getOrganizationUnitService(){

  //   this.isLoading = true;
  //   this.organizationUnitService.getList({
  //     limitedLevel:1
  //   }).subscribe({
  //     next: (res) => {
  //       this.isLoading = false;
  //       this.departments = res.items;
  //     },
  //     error: (err) => {
  //       this.isLoading = false;
  //     }
  //   })
  // }

}
