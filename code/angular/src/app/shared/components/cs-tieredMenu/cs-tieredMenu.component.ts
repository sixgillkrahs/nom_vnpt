import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'cs-tieredMenu',
  templateUrl: './cs-tieredMenu.component.html',
  styleUrls: ['./cs-tieredMenu.component.scss']
})
export class CsTieredMenuComponent implements OnInit {

  @Input() items: MenuItem[] = [];

  @Input() pTooltip: any;

  @Input() icon: any = "pi pi-cog";

  @Output() buttonItem: EventEmitter<any> = new EventEmitter();

    data: any;

    constructor() {}

    ngOnInit() {}
    
    dropdownItemsButton(data) {
        this.items = this.items;
        this.buttonItem.emit(data);
    }

}
