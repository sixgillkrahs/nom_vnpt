import { RoutesService, eLayoutType } from '@ctincsp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

const PROJECT_MANAGEMENT = [
  {
    path: undefined,
    name: 'Work::ProjectManagement',
    iconClass: '',
    order: 1,
    layout: eLayoutType.application,
  },
  {
    path: '/project-management/projects',
    name: 'Work::Pm:Project',
    iconClass: 'fas fa-home',
    order: 1,
    parentName: 'Work::ProjectManagement',
    layout: eLayoutType.application,
  },
  {
    path: '/project-management/project-role',
    name: 'Work::Pm:ProjectRole',
    iconClass: 'fas fa-home',
    order: 2,
    parentName: 'Work::ProjectManagement',
    layout: eLayoutType.application,
  },
  {
    path: '/project-management/project-state',
    name: 'Work::Pm:ProjectState',
    iconClass: 'fas fa-home',
    order: 3,
    parentName: 'Work::ProjectManagement',
    layout: eLayoutType.application,
  },

]

const WORK_MANAGEMENT = [
  {
    path: undefined,
    name: 'Work::WorkManagement',
    iconClass: '',
    order: 1,
    layout: eLayoutType.application,
  },
  {
    path: '/work-management/work-list',
    name: 'Work::WorkList',
    iconClass: 'fas fa-home',
    order: 1,
    parentName: 'Work::WorkManagement',
    layout: eLayoutType.application,
  },
]

const RESOURSE_MANAGEMENT = [
  {
    path: undefined,
    name: 'Work::ResourseManagement',
    iconClass: '',
    order: 1,
    layout: eLayoutType.application,
  },
  {
    path: '/resourse-management/hello',
    name: 'Work::DepartmentTeam',
    iconClass: 'fas fa-home',
    order: 1,
    parentName: 'Work::ResourseManagement',
    layout: eLayoutType.application,
  },
]


function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: undefined,
        name: 'Page',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/',
        name: 'Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        parentName: 'Page',
        layout: eLayoutType.application,
      },
      {
        path: undefined,
        name: 'Category::Category',
        iconClass: 'fas fa-home',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/category',
        name: 'Category::ManageCategory',
        iconClass: 'fas fa-home',
        order: 2,
        parentName: 'Category::Category',
        layout: eLayoutType.application,
        requiredPolicy: 'Categories'
      },
      {
        path: '/category/items/Work_Cat_Complexity',
        name: 'Work::Category:Complexity',
        iconClass: 'fas fa-home',
        order: 2,
        parentName: 'Category::Category',
        layout: eLayoutType.application,
      },
      {
        path: '/category/items/Work_Cat_DegreeOfImportant',
        name: 'Work::Category:DegreeOfImportant',
        iconClass: 'fas fa-home',
        order: 2,
        parentName: 'Category::Category',
        layout: eLayoutType.application,
      },
      {
        path: '/category/items/Work_Cat_Priority',
        name: 'Work::Category:Priority',
        iconClass: 'fas fa-home',
        order: 2,
        parentName: 'Category::Category',
        layout: eLayoutType.application,
      },
      {
        path: '/category/general-tree',
        name: 'Category::ManageGeneralTree',
        iconClass: 'fas fa-home',
        order: 2,
        parentName: 'Category::Category',
        layout: eLayoutType.application,
        requiredPolicy: 'GeneralTrees'
      },
      {
        path: '/category/general-tree/items/Work_Cat_WorkType',
        name: 'Work::Category:WorkType',
        iconClass: 'fas fa-home',
        order: 2,
        parentName: 'Category::Category',
        layout: eLayoutType.application,
      },
      {
        path: undefined,
        name: 'department',
        iconClass: 'fas fa-home',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/department-team',
        name: 'Work::DepartmentTeam',
        iconClass: 'fas fa-home',
        order: 2,
        parentName: 'department',
        layout: eLayoutType.application,
      },
      ...PROJECT_MANAGEMENT,
      ...WORK_MANAGEMENT,

      //quan
      ...RESOURSE_MANAGEMENT
    ]);
  };
}
