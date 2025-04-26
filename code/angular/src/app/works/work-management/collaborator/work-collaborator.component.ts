import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { LocalizationService, NotificationService } from '@ctincsp/ng.core';
import { IdentityUserDto, IdentityUserService } from '@ctincsp/ng.identity/proxy';
import { ConfirmationService } from 'primeng/api';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-work-collaborator',
  templateUrl: './work-collaborator.component.html',
  styleUrls: ['./work-collaborator.component.scss'],
})
export class WorkCollaboratorComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  @Input() header = '';
  @Input() collaborators: string;
  @Input() displayCollaborator: boolean = false;
  @Output() submitCollaborator = new EventEmitter<string[]>();
  @Output() closeCollaborator = new EventEmitter();

  users: IdentityUserDto[] = [];
  displayUserAssign: boolean = false;
  isLoading = false;
  arrCollaborators = [];
  totalRecords = 0;

  constructor(
    private identityUserService: IdentityUserService,
    private sanitizer: DomSanitizer,
    private localizationService: LocalizationService,
    private confirmationService: ConfirmationService,
    private notificationService: NotificationService
  ) {}

  ngOnInit() {
    this.arrCollaborators = this.collaborators ? this.collaborators.split(',') : [];
    this.getUsers();
  }
  getUsers() {
    if (!this.arrCollaborators?.length) {
      this.users = [];
      this.totalRecords = 0;
      return;
    }
    this.identityUserService
      .getListByIds(this.arrCollaborators)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: IdentityUserDto[]) => {
          this.users = response.map(x => {
            let avatarUrl = null;
            // if (x.avatarContent) {
            //   const objectURL = 'data:image/png;base64,' + x.avatarContent;
            //   avatarUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
            // }
            return {
              ...x,
              avatarUrl: avatarUrl,
            };
          });
          this.totalRecords = this.users.length;
          this.isLoading = false;
        },
        error: () => {
          this.isLoading = false;
        },
      });
  }

  showAddMember() {
    this.displayUserAssign = true;
  }
  closeUserAssign($event) {
    this.displayUserAssign = false;
  }
  addCollaborator(event) {
    if (event?.length) {
      this.arrCollaborators.push(...event);
      console.log(this.arrCollaborators);

      this.getUsers();
    }
    this.displayUserAssign = false;
  }
  deleteRow(user: IdentityUserDto) {
    if (!user) {
      this.notificationService.showError(
        this.localizationService.instant('AbpIdentity::Message:NotChoseAnyItem')
      );
      return;
    }

    this.confirmationService.confirm({
      message: this.localizationService.instant(
        'AbpIdentity::RoleDeletionConfirmationMessage',
        user.name
      ),
      accept: () => {
        this.isLoading = true;
        let index = this.arrCollaborators.indexOf(user.id);
        if (index > -1) {
          this.arrCollaborators.splice(index, 1);

          // this.notificationService.showSuccess(
          //   this.localizationService.instant('AbpIdentity::Message:DeleteOk')
          // );
        }
        this.isLoading = false;
      },
    });
  }
  save() {
    this.submitCollaborator.emit(this.arrCollaborators);
  }
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
