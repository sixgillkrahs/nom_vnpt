import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ConfigStateService } from '@ctincsp/ng.core';
import { TreeNode } from 'primeng/api';

@Component({
  selector: 'app-work-hand-over',
  templateUrl: './work-hand-over.component.html',
  styleUrls: ['./work-hand-over.component.scss'],
})
export class WorkHandOverComponent implements OnInit {
  @Input() header = '';
  @Input() displayHandOverWork: boolean = false;
  @Output() closehandOverWork = new EventEmitter();

  currentUser: any;

  constructor(
    private fb: FormBuilder,
    public config: ConfigStateService,
    ) {}

  form: FormGroup;

  selectedCategories: any[] = [];

    categories: any[] = [
        { name: 'Người thực hiện', key: 'A' },
        { name: 'Người liên quan', key: 'M' },
        { name: 'Người phê duyệt', key: 'P' },
    ];

    selectedStatus: any[] = [];

    status: any[] = [
        { name: 'Chưa hoàn thành', key: false },
        { name: 'Đã hoàn thành', key: false },
    ];

    delegateList: any[] = [];
    selectedDelegate: any[] | null;

    cities: any[] | undefined;

    selectedCity: any | undefined;

  ngOnInit() {
    this.buildForm();
    this.getCongViec();
    this.getCurrentUser();


    this.cities = [
      { name: 'New York', code: 'NY' },
      { name: 'Rome', code: 'RM' },
      { name: 'London', code: 'LDN' },
      { name: 'Istanbul', code: 'IST' },
      { name: 'Paris', code: 'PRS' }
  ];

  }

  buildForm() {
    this.form = this.fb.group({
      assigner: [],
    });
  }

  getCurrentUser(){
    this.config.getOne$('currentUser').subscribe(currUser => {
      this.currentUser = currUser;
    });
  }

  getCongViec(){
    this.delegateList = [
      {
        label: "Dự án Work",
        children: [
          {
            id: "abcd-defg-1abcd",
            label: "Viết SRS"
          },
          {
            id: "abcd-defg-2abcd",
            label: "Viết URD"
          },
          {
            id: "abcd-defg-3abcd",
            label: "Vẽ Prototype"
          }
        ]
      },
      {
        label: "Dự án IMS",
        children: [
          {
            id: "abcd-defg-4abcd",
            label: "Viết SRS"
          },
          {
            id: "abcd-defg-5abcd",
            label: "Viết URD"
          },
          {
            id: "abcd-defg-6abcd",
            label: "Vẽ Prototype"
          }
        ]
      }
    ]

    const selectedNode = ["abcd-defg-5abcd", "abcd-defg-2abcd"]

    this.selectedDelegate = this.getSelectedNodes(this.delegateList, selectedNode);

  }

  getSelectedNodes(nodes: any[], selectedNodeLabels: string[]): any[] {
    const selectedNodes: any[] = [];
    nodes.forEach(node => {
      if (selectedNodeLabels.includes(node.id)) {
        selectedNodes.push(node);
      }
      if (node.children) {
        const selectedChildren = this.getSelectedNodes(node.children, selectedNodeLabels);
        selectedNodes.push(...selectedChildren);
      }
    });
    console.log(selectedNodes);
    return selectedNodes;
  }

  selectionChange(event){
    console.log(event);
  }

  save() {}
}
