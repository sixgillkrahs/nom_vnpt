import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { PrimeIcons } from 'primeng/api';
import { ActivatedRoute, NavigationEnd, Route, RouteConfigLoadEnd, Router, Routes } from '@angular/router';
import { LocalizationService, RoutesService, UtilityService } from '@ctincsp/ng.core';
import { MenuData } from '../menu.component';
import { CreateUpdateMenuDto } from '@proxy/menu/dtos';

@Component({
  selector: 'app-add-menu',
  templateUrl: './add-menu.component.html',
  styleUrls: ['./add-menu.component.scss']
})
export class AddMenuComponent implements OnInit {

  formAdd: FormGroup = this.fb.group({});

  @Input() display: boolean = false;
  @Input() menuData?: MenuData = null;
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
    private util : UtilityService,
    private localizationService: LocalizationService,
    private router: Router,
    private routes: RoutesService,
    // private route: ActivatedRoute,
    private activatedRoute: ActivatedRoute
    ) {


  }

  get f() { return this.formAdd.controls; }

  ngOnInit() {
    this.createFormBuilder();

    this.getRouteProvider();

    this.getListIcons();

    this.getRoutes();

    this.getRouteLink();

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
      if(tree.path === "") return
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
          childNode.routerLink = `${subpath}/${child.path}`;
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
          childNode.label = `${this.localizationService.instant(child.data.name)} - ${subpath}/${child.path}`;
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


  createFormBuilder(){
    this.formAdd = this.fb.group({
      label: [null, Validators.required],
      iconClass: [null],
      requiredPolicy: [null],
      order: [null],
      routerLink: [null],
      url: [null],
      isGroup: [false],
      pathMode: [1],
      parentId: [this.menuData.parentId],
      node: [null]
    });

    this.modeConfig(1);

    if (!this.menuData.parentId) {
      this.formAdd.controls['isGroup'].patchValue(true);
      this.formAdd.controls['isGroup'].disable();
      this.f['label'].enable();
    }

    if(this.f['isGroup'].value){
      this.f['label'].enable();
    }else{
      this.f['label'].disable();
      this.f['node'].setValidators(Validators.required);
    }

    this.f['routerLink'].disable();

  }

  modeConfig(mode){
    let label = this.f['label'];
    let req = this.f['requiredPolicy'];
    let icon = this.f['iconClass'];
    let node = this.f['node'];

    if(mode === 2){
      label.enable();
    }else{
      label.disable();
    }

    label.reset();
    req.reset();
    icon.reset();
    node.reset();

  }

  changeMode(event){
    this.modeConfig(event.value);
    this.getListIcons();
  }

  changeGroup(event){
    this.f['node'].clearValidators();
    this.f['node'].reset();
    this.f['label'].reset();
    this.f['iconClass'].reset();
    this.f['requiredPolicy'].reset();
    this.f['order'].reset();

    if(!event.checked){
      this.f['node'].updateValueAndValidity();
      this.f['label'].disable();
    }else{
      this.f['node'].setValidators(Validators.required);
      this.f['label'].enable();
    }

    this.getRouteProvider();

  }

  filterIcon(event: AutoCompleteCompleteEvent) {
    let filtered: any[] = [];
    let query = event.query;

    for (let i = 0; i < (this.icons as any[]).length; i++) {
      let icon = (this.icons as any[])[i];
      if (icon.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
        filtered.push(icon);
      }
    }

    this.filteredIcons = filtered;
  }

  getListIcons() {
    const iconArray: any[] = [];

    for (const iconName in PrimeIcons) {
      if (Object.prototype.hasOwnProperty.call(PrimeIcons, iconName)) {
        iconArray.push({
          name: iconName,
          value: PrimeIcons[iconName],
        });
      }
    }

    this.icons = iconArray;

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

  // RouteConfig
  getRoutes(){
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
        name: x.data?.name
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
  //


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

  onNodeSelect(event: { originalEvent: Event, node: any }){
    const node = event.node;

    let newIcon = {
      name: node.icon,
      value: node.icon
    }

    this.formAdd.patchValue({
      label: this.localizationService.instant(node.name),
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

    this.util.markAllControlsAsDirty([this.formAdd]);
    if (this.formAdd.invalid) {
      return;
    }

    let value = this.formAdd.value;
    let valueControl = this.formAdd.controls
    if (value.pathMode == 1) {
      value.url = null;
    } else {
      value.routerLink = null;
    }

    if (!this.menuData.parentId) {
      value.isGroup = true;
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
      label: !valueControl.parentId.value ? valueControl.label.value : valueControl.node.value?.name,
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
