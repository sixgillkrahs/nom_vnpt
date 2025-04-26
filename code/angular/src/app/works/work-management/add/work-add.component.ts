import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DropdownItem } from '@ctincsp/ng.category';
import { LocalizationService, NotificationService, UtilityService } from '@ctincsp/ng.core';
import { ProjectGeneralService } from '@proxy/project-generals';
import { GetAllProjectGeneralDto } from '@proxy/project-generals/dtos';
import { CreateCspWorkDto, CspWorkDto, CspWorkService } from '@proxy/work-management';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-work-add',
  templateUrl: './work-add.component.html',
  styleUrls: ['./work-add.component.scss'],
})
export class WorkAddComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  @Input() parentWork: CspWorkDto = null;

  @Input() catPriority: DropdownItem[] = [];
  @Input() displayWorkAdd: boolean = false;

  @Output() closeWorkAdd = new EventEmitter();
  @Output() submitWorkAdd = new EventEmitter();

  dto: CreateCspWorkDto;
  form: FormGroup;
  get formControls() {
    return this.form.controls;
  }
  get CheckList() {
    return this.form.get('checkList') as FormArray;
  }
  projectOptions: any[] = [];
  isLoading: boolean = false;
  displayCollaborator: boolean = false;

  progress: number = 0;
  dragging: boolean = false;

  constructor(
    private fb: FormBuilder,
    private projectGeneralService: ProjectGeneralService,
    private cspWorkService: CspWorkService,
    private utilService: UtilityService,
    private notificationService: NotificationService,
    private localizationService: LocalizationService
  ) {}

  ngOnInit() {
    this.getProjects();
    this.buildForm();
  }
  getProjects() {
    this.isLoading = true;
    this.projectGeneralService
      .getDropdown({} as GetAllProjectGeneralDto)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        data => {
          this.projectOptions = data;
          this.isLoading = false;
        },
        () => {
          this.isLoading = false;
        }
      );
  }

  buildForm() {
    this.form = this.fb.group({
      workCode: [null, [Validators.required, Validators.maxLength(128)]],
      name: [null, [Validators.required, Validators.maxLength(255)]],
      taskCode: [null, [Validators.required, Validators.maxLength(128)]],
      startDate: [null],
      dueDate: [null],
      progress: [0, [Validators.min(0), Validators.max(100)]],
      duration: [null, [Validators.pattern(/^\d*\.?\d+(h|d|w)$/)]],
      priority: [null, [Validators.required]],
      complexity: [null],
      degreeOfImportant: [null],
      description: [null, [Validators.required, Validators.maxLength(500)]],
      owner: [null, [Validators.required]],
      assigner: [null],
      performer: [null],
      collaborators: [null],
      projectId: [null, [Validators.required]],
      restrictComplete: [false],
      checkList: this.fb.array([]),
      parentId: [this.parentWork?.id ?? null],
    });
  }

  addCheckList() {
    this.CheckList.push(
      this.fb.group({
        title: [null, [Validators.required, Validators.maxLength(256)]],
        isCompleted: [false],
      })
    );
  }
  deleteCheckList(index) {
    this.CheckList.removeAt(index);
  }

  openCollaborator() {
    this.displayCollaborator = true;
  }
  submitCollaborator(event) {
    let colls = [...event];
    this.form.get('collaborators').setValue(colls.join(','));
    this.displayCollaborator = false;
  }
  closeCollaborator(event) {
    this.displayCollaborator = false;
  }

  save() {
    this.utilService.markAllControlsAsDirty([this.form]);
    if (this.form.invalid) {
      this.notificationService.showWarn(
        this.localizationService.instant('AbpIdentity::Message:FormInvalid')
      );
      return;
    }
    this.isLoading = true;
    this.cspWorkService
      .create(this.form.value)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        res => {
          this.isLoading = false;
          this.displayCollaborator = false;
          this.notificationService.showSuccess(
            this.localizationService.instant('AbpIdentity::Message:CreateOk')
          );
          this.submitWorkAdd.emit(res);
        },
        err => {
          this.isLoading = false;
        }
      );
  }

  onMouseDown(event: MouseEvent) {
    this.dragging = true;
    this.updateProgress(event);
  }

  onMouseMove(event: MouseEvent) {
    if (this.dragging) {
      this.updateProgress(event);
    }
  }

  onMouseUp() {
    this.dragging = false;
  }

  updateProgress(event: MouseEvent) {
    const progressBarWidth = document.querySelector('.p-progressbar').clientWidth;
    const clickX =
      event.clientX - document.querySelector('.p-progressbar').getBoundingClientRect().left;
    let percentage = Math.min(100, Math.max(0, (clickX / progressBarWidth) * 100));
    percentage = parseFloat(percentage.toFixed(2));
    this.progress = percentage;
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
