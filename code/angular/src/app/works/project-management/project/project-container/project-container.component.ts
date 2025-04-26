import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProjectDetailData } from 'src/app/shared/ProjectManager/models/project-manager.model';

@Component({
  selector: 'app-project-container',
  templateUrl: './project-container.component.html',
  styleUrls: ['./project-container.component.scss']
})
export class ProjectContainerComponent implements OnInit {
  @Input() displayProjectContainer: boolean = false;
  @Input() projectDetailData: ProjectDetailData;
  @Output() closeProjectContainer = new EventEmitter();

  isLoading: boolean = false;

  selectedIndex = 0;

  constructor() { }

  ngOnInit() {
    this.selectedIndex = this.projectDetailData.active;
  }

  tabViewIndex(event) {
    this.selectedIndex = event.index;
  }

}
