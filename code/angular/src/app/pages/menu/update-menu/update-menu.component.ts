import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LocalizationService, RoutesService, UtilityService } from '@ctincsp/ng.core';
import { CreateUpdateMenuDto, MenuDto } from '@proxy/menu/dtos';
import { PrimeIcons } from 'primeng/api';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { MenuData } from '../menu.component';

@Component({
  selector: 'app-update-menu',
  templateUrl: './update-menu.component.html',
  styleUrls: ['./update-menu.component.scss']
})
export class UpdateMenuComponent implements OnInit {

  formUpdate: FormGroup = this.fb.group({});
  @Input() display: boolean = false;
  @Input() menuData: MenuData;
  @Input() isLoading: boolean = false;
  @Output() appClose: EventEmitter<any> = new EventEmitter();
  @Output() appSubmit: EventEmitter<any> = new EventEmitter();

  icons: any[] = [];
  filteredIcons: any[] = [];
  pathOptions: any[] = [{ label: 'CSP', value: 1 }, { label: 'Url', value: 2 }];
  routerLinks: any[] = [];

  routeProvider: any[] = [];
  routeLinks: any[] = [];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private util : UtilityService,
    private localizationService: LocalizationService,
    private routes: RoutesService,
    ) {
  }

  get f() { return this.formUpdate.controls; }

  ngOnInit() {
    this.createFormBuilder();
    this.getListIcons();
    this.getRoutes();
    this.getRouteProvider();
    this.getRouteLink();
  }

  createFormBuilder(){
    this.formUpdate = this.fb.group({
      name: [],
      label: [null, [Validators.required]],
      iconClass: [null, []],
      requiredPolicy: [null, []],
      order: [null, []],
      routerLink: [null, []],
      url: [null, []],
      isGroup: [false, []],
      pathMode: [1, []],
      parentId: [null, []],
      attributeRoute: [null],
      node: [null]
    });

    this.f['routerLink'].disable();

    this.formUpdate.patchValue({
      ...this.menuData.param,
      label: this.menuData.param.label,
      name: this.localizationService.instant(this.menuData.param.label),
    });
    let findIcon = this.icons.find(x => x.value == this.menuData.param.iconClass);
    if (findIcon) {
      this.formUpdate.controls['iconClass'].patchValue({ name: findIcon.name, value: this.menuData.param.iconClass });
    } else {
      this.formUpdate.controls['iconClass'].patchValue({ name: this.menuData.param.iconClass, value: this.menuData.param.iconClass });
    }

    if (!this.menuData.param.isGroup && this.menuData.param.url) {
      this.formUpdate.controls['pathMode'].patchValue(2);
    }

    this.formUpdate.controls['isGroup'].disable();

    if(!this.f['isGroup'].value){
      this.f['name'].disable();
    }else{
      this.f['name'].enable();
    }
  }

  modeConfig(mode){
    let label = this.f['name'];
    let req = this.f['requiredPolicy'];
    let icon = this.f['iconClass'];
    let node = this.f['node'];

    if(mode === 2){
      label.enable();
    }else{
      label.disable();
    }

    // label.reset();
    // req.reset();
    // icon.reset();
    // node.reset();

  }

  changeMode(event){
    this.modeConfig(event.value);
    this.getListIcons();
  }

  filterIcon(event: AutoCompleteCompleteEvent) {
    let filtered: any[] = [];
    let query = event.query;

    for (let i = 0; i < (this.icons as any[]).length; i++) {
      let icon = (this.icons as any[])[i];
      if (icon.name?.toLowerCase().indexOf(query.toLowerCase()) == 0) {
        filtered.push(icon);
      }
    }

    this.filteredIcons = filtered;
  }


  addIcon(){
    let iconClass:any;

    if(this.f['iconClass'].value){
      iconClass = this.f['iconClass'].value;
    }else{
      iconClass = null;
    }

    let x:any = this.icons.find(icon => icon.value === iconClass);

    if(!x){
      this.icons.push(iconClass)
    }
  }

  getListIcons() {
    const iconArray: any[] = [];
    let iconClass = this.f['iconClass'].value;

    for (const iconName in PrimeIcons) {
      if (Object.prototype.hasOwnProperty.call(PrimeIcons, iconName)) {
        iconArray.push({
          name: iconName,
          value: PrimeIcons[iconName],
        });
      }
    }

    this.icons = iconArray;

    this.addIcon();

  }

  getRoutes() {
    const config = this.router.config;

    let rootConfig = {
      path: "",
      children: config
    };
    let rootNode: any = {};
    this.readRouteConfigNode(rootConfig, rootNode);
    let leafPaths = this.findLeafPaths(rootNode.children).map(x => {
      return {
        path: x.path,
        name: x.data?.name ?? x.path
      }
    });
    this.routerLinks = leafPaths;
  }

  readRouteConfigNode(configSource: any, treeNodeTarget: any) {
    treeNodeTarget.path = configSource.path;
    treeNodeTarget.data = configSource.data;
    treeNodeTarget.children = [];
    if (configSource._loadedRoutes) {
      configSource._loadedRoutes.forEach(childConfig => {
        let childNode = {};
        this.readRouteConfigNode(childConfig, childNode);
        treeNodeTarget.children.push(childNode);
      });
    }

    if (configSource.children) {
      configSource.children.forEach(childConfig => {
        let childNode = {};
        this.readRouteConfigNode(childConfig, childNode);
        treeNodeTarget.children.push(childNode);
      });
    }
  }

  findLeafPaths(tree: any[], currentPath: string = ''): { path: string, data: any }[] {
    let leafPaths: { path: string, data: any }[] = [];

    for (const node of tree) {
      const path = this.cleanPath(currentPath + '/' + node.path);

      if (node.children.length === 0) {
        leafPaths.push({ path: path, data: node.data });
      } else {
        leafPaths = leafPaths.concat(this.findLeafPaths(node.children, path));
      }
    }

    return leafPaths;
  }

  cleanPath(path: string): string {
    return path.replace(/\/+/g, '/').replace(/\/$/, '');
  }


  getRouteProvider(){
    const arrayModel = [];
    this.routes.visible.forEach(tree => {
      const modelApb:any = {
        label: this.localizationService.instant(tree.name),
        name: tree.name,
        icon: tree.iconClass,
        children: this.getChildNode(tree.children)
      }

      if(tree.requiredPolicy){
        modelApb.requiredPolicy = tree.requiredPolicy;
      }

      if(modelApb.children.length > 0){
        modelApb.childBool = true;
      }else{
        modelApb.childBool = false;
      }

      if(modelApb.childBool){
        modelApb.label = this.localizationService.instant(tree.name);
        modelApb.selectable = false;
      }

      if(modelApb.childBool && this.f['isGroup'].value){
        modelApb.selectable = true;
      }
      if(modelApb.childBool && !this.f['isGroup'].value){
        modelApb.selectable = false;
      }

      if(!modelApb.childBool && this.f['isGroup'].value){
        modelApb.selectable = false;
      }
      if(!modelApb.childBool && !this.f['isGroup'].value){
        modelApb.selectable = true;
      }

      arrayModel.push(modelApb)

    });

    this.routeProvider = arrayModel;
  }

  getChildNode(childItem:any){

    const childNodes = childItem.filter(child => child.path || (child.children && child.children.length > 0));
    return childNodes.map(child => {
      const childNode:any = {
        name: child.name,
        icon: child.iconClass
      };

      if (child.path) {
        childNode.routerLink = child.path;
      }

      if (child.children && child.children.length > 0) {
        childNode.children = this.getChildNode(child.children);
        childNode.childBool = true;
      }else{
        childNode.childBool = false
      }

      if(child.requiredPolicy){
        childNode.requiredPolicy = child.requiredPolicy;
      }

      if(childNode.childBool){
        childNode.label = this.localizationService.instant(child.name);
        childNode.selectable = false;
      }else{
        childNode.label = `${this.localizationService.instant(child.name)} - ${child.path}`;
      }

      if(childNode.childBool && this.f['isGroup'].value){
        childNode.selectable = true;
      }
      if(childNode.childBool && !this.f['isGroup'].value){
        childNode.selectable = false;
      }

      if(!childNode.childBool && this.f['isGroup'].value){
        childNode.selectable = false;
      }
      if(!childNode.childBool && !this.f['isGroup'].value){
        childNode.selectable = true;
      }

      return childNode;
    })
  }

  getRouterConfig(){
    let routers = [];
    const routerConfig = this.router.config.filter(r => r.path === "");

    routerConfig.forEach((result:any) => {

      if(result._loadedRoutes){
        result._loadedRoutes.forEach((group:any) => {
          routers.push(group);
        });
      }

    })

    return routers;

  }

  getRouteLink(){

    const arrayModel = [];
    this.getRouterConfig().forEach((tree:any) => {
      const modelRoute:any = {
        label: tree.data ? this.localizationService.instant(tree.data.name) : null,
        name: tree.data ? tree.data.name : null,
        children: this.getChildRouteLink(tree.children, tree.path)
      }

      if(modelRoute.children?.length > 0){
        modelRoute.childBool = true;
      }else{
        modelRoute.childBool = false;
      }

      if(modelRoute.childBool){
        modelRoute.selectable = false;
      }

      if(modelRoute.childBool && this.f['isGroup'].value){
        modelRoute.selectable = true;
      }
      if(modelRoute.childBool && !this.f['isGroup'].value){
        modelRoute.selectable = false;
      }

      if(!modelRoute.childBool && this.f['isGroup'].value){
        modelRoute.selectable = false;
      }
      if(!modelRoute.childBool && !this.f['isGroup'].value){
        modelRoute.selectable = true;
      }

      arrayModel.push(modelRoute);
    })

    this.routeLinks = arrayModel;
  }

  getChildRouteLink(childItem:any, subpath?){
      return childItem?.map(child => {
        const childNode:any = {
          label: child.data ? `${this.localizationService.instant(child.data.name)} - /${child.path}` : null,
          name: child.data ? child.data.name : null,
        };

        if (child.path) {
          childNode.routerLink = `/${subpath}/${child.path}`;
        }

        if (child?.children && child.children?.length > 0) {
          childNode.children = this.getChildRouteLink(child.children);
          childNode.childBool = true;
        }else{
          childNode.childBool = false
        }

        if(child.requiredPolicy){
          childNode.requiredPolicy = child.requiredPolicy;
        }

        if(childNode.childBool){
          childNode.label = this.localizationService.instant(child.data.name);
          childNode.selectable = false;
        }else{
          childNode.label = `${this.localizationService.instant(child.data.name)} - /${subpath}/${child.path}`;
        }

        if(childNode.childBool && this.f['isGroup'].value){
          childNode.selectable = true;
        }
        if(childNode.childBool && !this.f['isGroup'].value){
          childNode.selectable = false;
        }

        if(!childNode.childBool && this.f['isGroup'].value){
          childNode.selectable = false;
        }
        if(!childNode.childBool && !this.f['isGroup'].value){
          childNode.selectable = true;
        }

        return childNode;
      })
  }

  onNodeSelect(event: { originalEvent: Event, node: any }){
    const node = event.node;

    let newIcon = {
      name: node.icon,
      value: node.icon
    }

    this.formUpdate.patchValue({
      label: node.name,
      name: this.localizationService.instant(node.name),
      iconClass: newIcon,
      requiredPolicy: node.requiredPolicy,
      order: node.order ? node.order : null,
      routerLink: node.routerLink ? node.routerLink : undefined
    })


    this.getListIcons();

    if(this.f['iconClass'].value.name){
      this.addIcon();
    }

  }

  save() {
    this.util.markAllControlsAsDirty([this.formUpdate]);
    if (this.formUpdate.invalid) {
      return;
    }

    let value = this.formUpdate.value;
    let valueControl = this.formUpdate.controls

    value.isGroup = this.menuData.param.isGroup;

    if (value.pathMode == 1) {
      value.url = null;
    } else {
      value.routerLink = null;
    }

    if (value.iconClass){
      if(value.iconClass.name){
        value.iconClass = value.iconClass.value;
      }else{
        value.iconClass = null;
      }
    }else{
      value.iconClass = null;
    }

    let newData: CreateUpdateMenuDto = {
      label: valueControl.name.value,
      iconClass: value.iconClass,
      requiredPolicy: valueControl.requiredPolicy.value,
      order: valueControl.order.value,
      routerLink: valueControl.routerLink.value,
      url: valueControl.url.value,
      isGroup: valueControl.isGroup.value,
      parentId: valueControl.parentId.value,
    }

    this.appSubmit.emit(newData);

  }

}
