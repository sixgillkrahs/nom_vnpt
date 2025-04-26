import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-state',
  templateUrl: './state.component.html',
  styleUrls: ['./state.component.scss']
})
export class StateComponent implements OnInit {

  states: any[] = [];

  constructor() { }

  ngOnInit() {
    this.states = [
      {
        id: "dsdsd-sd-sdwe",
        state: "Khởi tạo",
        alias: "Khởi tạo dự án",
      },
      {
        code: "hehe-htht-htg4",
        state: "Đấu thầu",
        alias: "Presale",
      }
     ];
  }

}
