import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MenuItem, PrimeNGConfig } from 'primeng/api';
import { MenuService } from '@proxy/menu';
import { NavItemsService, UserMenuService } from '@ctincsp/ng.theme.shared';
import { AbpLocalStorageService, AuthService, ConfigStateService,  NotificationService, RoutesService } from '@ctincsp/ng.core';
import { DialogService } from 'primeng/dynamicdialog';

import { DIALOG_MD, DIALOG_SM } from 'src/app/shared/constants/sizes.const';

//import { ChangePasswordInput } from '@proxy/ctin/abp/account';
import { IdentityUserDto } from '@ctincsp/ng.identity/proxy';
import { SettingTabsService } from '@ctincsp/ng.setting-management/config';
import { MySettingsComponent } from './my-settings/my-settings.component';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [NotificationService, ConfirmationService]
})
export class AppComponent implements OnInit {

  items: MenuItem[] = [];
  menuApb: boolean = false;
  blockScreen: boolean = false;
  currentUser: any;

  menuItem = {
    totalCount: 1,
    items: [
      {
        routerLink: null,
        url: null,
        label: "Bàn làm việc",
        order: 2,
        iconClass: "",
        requiredPolicy: null,
        layout: null,
        parentId: null,
        isGroup: true,
        clientId: "Work_App",
        id: "table1"
      },
      {
        routerLink: "/identity/roles",
        url: null,
        label: "Tạo mới công việc",
        order: 1,
        iconClass: "pi pi-users",
        requiredPolicy: null,
        layout: null,
        parentId: "table1",
        isGroup: false,
        clientId: "Work_App",
        id: "work1"
      }
    ]
  }

  constructor(
    private primengConfig: PrimeNGConfig,
    private menuService: MenuService,
    private authService: AuthService,
    private notificationService: NotificationService,
    public config: ConfigStateService,
    public dialogService: DialogService,

    private routes: RoutesService,
    private router: Router,
    private route: ActivatedRoute,

    private settingTabs: SettingTabsService,
    private localStorageService: AbpLocalStorageService,
  ) {

    settingTabs.add([
      {
        name: 'MySettings',
        order: 1,
        requiredPolicy: 'policy key here',
        component: MySettingsComponent,
      },
    ]);

  }

  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }

  ngOnInit() {
    this.config.getOne$('currentUser').subscribe(currUser => {
      this.currentUser = currUser;
    });
    this.primengConfig.ripple = true;
    this.getMenu();
    this.getNavMenu();
  }

  getMenu() {
    // this.userMenuService.menuUser$.next(this.items);
  }


  getNavMenu() {

    this.menuService.getList({ clientId: 'Work_App' }).subscribe({
      next: (res) => {
        const root = this.convertListToTree(res.items);
        this.routes.navMenuService$.next(root.items);
      },
      error: (err) => {

      }
    });
  }

  completeNode(id: string, node: any, list: any[]) {
    node.items = list.filter(x => x['parentId'] == id).sort(x => x.order).map(x => {
      if (x.isGroup) {
        let childNode = {
          label: x.label,
          icon: x.iconClass,
          items: []
        };
        this.completeNode(x.id, childNode, list);
        return childNode;
      } else {
        if (x.url) {
          return {
            label: x.label,
            icon: x.iconClass,
            url: x.url
          };
        } else {
          return {
            label: x.label,
            icon: x.iconClass,
            routerLink: [x.routerLink]
          };
        }
      }
    });
  }

  convertListToTree<T>(list: T[]) {
    let rootNode = { items: [] };
    this.completeNode(null, rootNode, list);
    return rootNode;
  }

  login() {
    this.authService.navigateToLogin();
  }

}
