
import { Component, HostListener, Injector, OnInit } from '@angular/core';
import { LocalizationService, NotificationService, ReponsiveService } from '@ctincsp/ng.core';
import { MenuService } from '@proxy/menu';
import { CreateUpdateMenuDto, MenuDto } from '@proxy/menu/dtos';
import { ConfirmationService, MessageService, TreeNode } from 'primeng/api';
import { Util } from 'src/app/shared/util';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss'],
  providers: [ReponsiveService]
})
export class MenuComponent implements OnInit {

  menus!: TreeNode[];

  isLoading: boolean = false;

  isvisibleAdd: boolean = false;
  isvisibleUpdate: boolean = false;
  parentId: string = null;

  menuData: MenuData;

  editingMenu: MenuDto;
  clientIds: string[] = [];
  clientId: string = "Work_App";

  screen: ReponsiveService

  constructor(
    injector: Injector,
    private menuService: MenuService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private localizationService: LocalizationService,
    private notificationService: NotificationService,
  ) {
    this.screen = injector.get(ReponsiveService);
  }

  ngOnInit() {
    this.getListClientId();
    this.getListMenu();
  }

  getListClientId() {
    this.menuService.getListClientId().subscribe(res => {
      this.clientIds = res;
    });
  }

  getListMenu() {
    this.menuService.getList({ clientId: this.clientId }).subscribe(res => {
      let root = this.convertListToTree(res.items);
      this.menus = root.children;
    }, err => {

    });
  }

  changeClient() {
    this.getListMenu();
  }

  completeNode(node: any, list: any[]) {
    node.children = list.filter(x => x['parentId'] == node.data.id).sort(x => x.order).map(x => {
      let childNode = {
        data: x,
        children: []
      };
      this.completeNode(childNode, list);
      return childNode;
    });
  }

  convertListToTree<T>(list: T[]) {

    let rootNode = { data: { id: null }, children: [] };
    this.completeNode(rootNode, list);
    
    return rootNode;
  }

  showAddModal(value: MenuDto){

    this.isvisibleAdd = true;

    let data:MenuData = {
      header: this.localizationService.instant("Work::Menu:New"),
      parentId: null
    }

    if(value){
      data.header = this.localizationService.instant("Work::Menu:Sub.New"),
      data.parentId = value.id
    }

    this.menuData = data;
  }

  addMenu(value: CreateUpdateMenuDto) {
    this.isLoading = true;
    this.menuService.create({ ...value, clientId: this.clientId }).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.isvisibleAdd = false;
        this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Create.Ok", value.label));
        this.getListMenu();
      },
      error: (err) => {
        this.isLoading = false;
        this.notificationService.showError(err.error.error.message);
      }
    })
  }

  showEditModal(value: MenuDto){
    this.editingMenu = value;
    this.isvisibleUpdate = true;

    let data:MenuData = {
      header: this.localizationService.instant("Work::Edit"),
      param: value,
      parentId: value.parentId
    }

    this.menuData = data;
  }

  updateMenu(value: CreateUpdateMenuDto) {
    this.isLoading = true;
    this.menuService.update(this.editingMenu.id, { ...value, clientId: this.clientId }).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.isvisibleUpdate = false;
        this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Update.Ok", value.label));
        this.getListMenu();
      },
      error: (err) => {
        this.isLoading = false;
        this.notificationService.showError(err.error.error.message);
      }
    })
  }

  deleteMenu(data: any) {
    this.confirmationService.confirm({
      message: this.localizationService.instant('Work::Message:Confirm.Delete', this.localizationService.instant(data.label) ),
      accept: () => {
        this.menuService.delete(data.id).subscribe(res => {
          this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Delete.Ok", this.localizationService.instant(data.label)));
          this.getListMenu();
        });
      }
    });

  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.screen.setScreenSize();
  }

}

export class MenuData {
  header: string;
  parentId?: any;
  param?: MenuDto;
}
