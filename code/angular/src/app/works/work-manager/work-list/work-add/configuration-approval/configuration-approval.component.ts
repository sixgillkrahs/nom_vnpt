
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DepartmentTeamDetailData } from 'src/app/shared/DepartmentTeam/models/department-team.model';
import { FormBuilder, FormGroup, Validators, FormsModule, ReactiveFormsModule, FormControl } from '@angular/forms';
import { OrganizationUnitCreateDto, OrganizationUnitDto, OrganizationUnitService, OrganizationUnitLookupDto, IdentityUserService, IdentityUserDto, GetIdentityUsersInput } from '@ctincsp/ng.identity/proxy';
import { NgModule } from '@angular/core';
import { DepartmentTeamService } from '@proxy/department-teams';
import { CreateUpdateDepartmentTeamDto, DepartmentTeamDto } from '@proxy/department-teams/dtos';
import { isNullOrUndefined } from '@ctincsp/ng.core';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService, TreeNode } from 'primeng/api';
import { LocalizationService, NotificationService,UtilityService,PagedResultDto  } from '@ctincsp/ng.core';
import { PickListModule } from 'primeng/picklist';
import { SelectItem } from 'primeng/api';
import { switchMap, tap } from 'rxjs/operators';
interface Employee {
  name: string;
  position: string;
  department: string;
  editMode?: boolean; // Track edit mode
}

@Component({
  selector: 'app-configuration-approval',
  templateUrl: './configuration-approval.component.html',
  styleUrls: ['./configuration-approval.component.scss']
})
export class ConfigurationApprovalComponent implements OnInit {
  @Input() displayApproval: boolean = false;
  @Input() departmentTeamDetailData: DepartmentTeamDetailData;
  @Output() closeContainer = new EventEmitter();
 // organizationLookupData: OrganizationUnitLookupDto[] = [];
  organizationLookupData: OrganizationUnitDto[] = [];
  selectedUserName: string;
  selectedUserName2 :string;
  selectedUser: IdentityUserDto[] = [];
  selectedUsers: any[] = [];
  targetUsers: any[];
  selectedUsersTemp:IdentityUserDto[] = [];
  resultList: any[] = [];

  isLoading: boolean = false;
  formGroup!: FormGroup;
  limitedLevel: number = 1;
  selectedIndex = 0;
  selectedOrganization: OrganizationUnitLookupDto | null = null;
  departmentTeam: DepartmentTeamDto[] = [];
  //constructor(private fb: FormBuilder) { }
  newDepartmentTeam: CreateUpdateDepartmentTeamDto = {
    code: '', // Đảm bảo điền đầy đủ thông tin cần thiết
    name: '',
    departmentID: null,
    description: '',
    users: [], // Điền danh sách người dùng (users) theo định dạng của API
  };
  selectedDepartmentID: any;
  employees: Employee[] = [];
  showSecondListbox: boolean = false;
  checked: boolean = false;
  constructor(
    private fb: FormBuilder,
    private organizationUnitService: OrganizationUnitService,
    private identityUserService: IdentityUserService,
    private departmentTeamService: DepartmentTeamService,
    private router: Router,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private localizationService: LocalizationService,
    private notificationService: NotificationService,
    private util : UtilityService

  ) {
    this.formGroup = this.fb.group({
      code: [null, Validators.required],
      name: [null, Validators.required],
      departmentID: [null, Validators.required],
      description: [null],
      users: [],
    });

    this.newDepartmentTeam = {
      code: '',
      name: '',
      departmentID: '',
      description: '',
      users: [],
    };

  }
  onModalClosedCallback: () => void;
  get f() { return this.formGroup.controls; }
  ngOnInit() {
    this.selectedIndex = this.departmentTeamDetailData.active;
    this.loadOrganizationLookup();

    if (this.departmentTeamDetailData.id) {
      // Nếu có giá trị id, đây là chế độ edit
      this.getdepartmentTeamByID();
      this.loadUserEdit();
    } else {
      this.loadUser();
      // Ngược lại, đây là chế độ thêm mới
      // Thực hiện các bước cần thiết cho chế độ thêm mới
    }
    this.targetUsers = [];
  }

  createFormBuild() {
    this.formGroup = this.fb.group({
      code: ["", Validators.required],
      name: ["", Validators.required],
      departmentID: [null, Validators.required],
    })
  }
  tabViewIndex(event) {
    this.selectedIndex = event.index;
  }
  loadOrganizationLookup(): void {
    // this.organizationUnitService.getLookup(this.limitedLevel).subscribe((data) => {
    //   this.organizationLookupData = data;
    // });

    this.organizationUnitService.getList({ maxResultCount: 100 }).subscribe((data: PagedResultDto<OrganizationUnitDto>) => {
      if (data && data.items) {
        this.organizationLookupData = data.items;
      } else {
        console.error('Invalid API response structure');
      }
    });
  }

  loadUserEdit(): void {
    this.identityUserService.getList({ maxResultCount: 10 }).subscribe(res => {
      console.log("user", res);

      if (res && res.items) {
        this.selectedUser = res.items;
        this.selectedUser = this.selectedUsersTemp.filter(item => !this.resultList.some(resultItem => resultItem.id === item.id));
      } else {
        // Xử lý trường hợp không tìm thấy danh sách người dùng trong đối tượng trả về
        console.error('Invalid API response structure');
      }
    });
  }
  loadUser(): void {
    this.identityUserService.getList({ maxResultCount: 10 }).subscribe(res => {
      console.log("user", res);

      if (res && res.items) {
        this.selectedUser = res.items;
      } else {
        // Xử lý trường hợp không tìm thấy danh sách người dùng trong đối tượng trả về
        console.error('Invalid API response structure');
      }
    });
  }
  onUserChange(event: any) {
    this.selectedUserName = event.value;
  }
  onUser2Change(event: any) {
    this.selectedUserName2 = event.value;
  }
  onShowListbox2(event: any) {
    // Update selectedUser when an item is selected
   this.showSecondListbox =true
  }
  onSelectedUsersChange(event: any): void {
    // Xử lý logic khi danh sách được chọn thay đổi
    console.log("onchange", event); // event chính là giá trị mới của selectedUsers
  }

  resetForm() {
    this.formGroup.reset();
  }

  getdepartmentTeamByID() {
    this.displayApproval = true;
    // this.isLoading = true;
    this.identityUserService.getList({ maxResultCount: 10 })
      .pipe(
        switchMap(res => {
          if (res && res.items) {
            this.selectedUsersTemp = res.items;
          } else {
            console.error('Invalid API response structure');
          }
          console.log("selectedUsersTemp", this.selectedUsersTemp);

          // Tiếp tục với lấy chi tiết nhóm sau khi có danh sách người dùng
          return this.departmentTeamService.get(this.departmentTeamDetailData.id);
        }),
        tap(team => {
          this.formGroup.patchValue({
            code: team.code,
            name: team.name,
            departmentID: team.departmentID,
            description: team.description
            // Gắn giá trị các trường khác tương tự
          });
          this.targetUsers = (team as any).lstMember || [];

          // Lặp qua từng phần tử trong lstmember
          for (let i = 0; i < this.targetUsers.length; i++) {
            // Tìm kiếm phần tử có giá trị trùng với trường 'value' trong lstmember
            console.log("this.selectedUser tron for", this.selectedUsersTemp);
            const lstMemberItem = this.targetUsers[i];
            const matchedItem = this.selectedUsersTemp.find(item => item.id.trim() === String(lstMemberItem.value).trim());
            console.log("String(lstMemberItem.value)", String(lstMemberItem.value));
            console.log("matchedItem", matchedItem);
            // Nếu tìm thấy phần tử trùng
            if (matchedItem) {
              // Thêm phần tử vào resultList
              this.resultList.push({ ...matchedItem });
            }
          }
          this.selectedUser =[];
          this.selectedUser = this.selectedUsersTemp.filter(item => !this.resultList.some(resultItem => resultItem.id === item.id));
          console.log("this.selectedUser",this.selectedUser);
          this.targetUsers = this.resultList;
          console.log("this.resultList", this.resultList);
        })
      )
      .subscribe(
        () => { },
        error => {
          this.isLoading = false;
        }
      );
      // setTimeout(() => {
      //   // Kết thúc loading khi tải dữ liệu xong
      //   this.isLoading = false;
      // }, 2000);
  }

  // getListClientId() {

  //   this.departmentTeamService.getListClientId({ filter: '', departmentID: null, maxResultCount: 10 }).subscribe(res => {
  //     this.departmentTeam = res.items;

  //   });
  // }
  createDepartmentTeam(): void {
    this.util.markAllControlsAsDirty([this.formGroup]);
    if (this.formGroup.invalid) {
      return;
    }
    this.isLoading = true;
    if (this.formGroup.valid) {
      if (this.departmentTeamDetailData.id !== null && this.departmentTeamDetailData.id !== undefined) {
        this.newDepartmentTeam = this.formGroup.value;
        //this.newDepartmentTeam.departmentID = this.formGroup.value.departmentID.id;
        if (this.newDepartmentTeam.users = null)
          this.newDepartmentTeam.users = []
        else {
          const selectedUserIds: string[] = this.targetUsers
            .filter(item => item.value && item.value.id) // Lọc những phần tử không có giá trị hoặc id
            .map(item => item.value.id.toString());

          for (let i = 0; i < this.targetUsers.length; i++) {
            const item = this.targetUsers[i];
            if (item.id !== "") {
              selectedUserIds.push(item.id.toString());
            }
          }
          this.newDepartmentTeam.users = selectedUserIds;
        }

        if (this.formGroup.value.description == null)
          this.newDepartmentTeam.description = "";
        this.departmentTeamService.update(this.departmentTeamDetailData.id, this.newDepartmentTeam).subscribe({
          next: (result) => {
            // Handle success
            this.displayApproval = false;
            this.closeContainer.emit(true);
            // setTimeout(() => {
            //   // Kết thúc loading khi tải dữ liệu xong
            //   this.isLoading = false;
            //   this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Update.Ok", result.name));
            // }, 2000);
            this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Update.Ok", result.name));
          },
          error: (error) => {
            // Handle error
          }
        });
      }
      else {
        this.newDepartmentTeam = this.formGroup.value;
        ///this.newDepartmentTeam.departmentID = this.formGroup.value.departmentID;
        if (this.newDepartmentTeam.users = null)
          this.newDepartmentTeam.users = []
        else {
          const selectedUserIds: string[] = this.targetUsers
            .filter(item => item.value && item.value.id) // Lọc những phần tử không có giá trị hoặc id
            .map(item => item.value.id.toString());

          for (let i = 0; i < this.targetUsers.length; i++) {
            const item = this.targetUsers[i];
            if (item.id !== "") {
              selectedUserIds.push(item.id.toString());
            }
          }
          this.newDepartmentTeam.users = selectedUserIds;
        }
        if (this.formGroup.value.description == null)
          this.newDepartmentTeam.description = "";
        this.departmentTeamService.create(this.newDepartmentTeam).subscribe({
          next: (result) => {
            // Handle success
            this.displayApproval = false;
            this.closeContainer.emit(true);
            // setTimeout(() => {
            //   // Kết thúc loading khi tải dữ liệu xong
            //   this.isLoading = false;
            //   this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Create.Ok", result.name));
            // }, 2000);
            this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Create.Ok", result.name));
          },
          error: (error) => {
            // Handle error
          }
        });
      }
    }

  }
}
