import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { filter } from 'rxjs';
import { BreadcrumDefineService } from 'src/app/shared/services/BreadcrumDefine.service';



@Component({
    selector: 'app-resourse-management',
    templateUrl: './resourse-management.component.html',
    styleUrls: ['./resourse-management.component.css'],
    providers: [BreadcrumDefineService]
  })


export class ResourseManagementComponent implements OnInit {


  items: MenuItem[] | undefined;

  home: MenuItem | undefined;

  constructor(private router: Router,private route: ActivatedRoute, breadcrum: BreadcrumDefineService) {
    this.router.events.pipe(filter((event) => event instanceof NavigationEnd)).subscribe(event => {

      const root = this.router.routerState.snapshot.root;

      const routeConfig = root.children[0].routeConfig;

      this.items = breadcrum.getBreadcrum(routeConfig);
    });
   }

  ngOnInit() {
    this.home = { icon: 'pi pi-home', routerLink: '/' };
  }

}