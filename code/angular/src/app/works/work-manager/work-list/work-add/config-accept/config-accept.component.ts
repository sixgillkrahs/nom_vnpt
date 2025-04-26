import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-config-accept',
  templateUrl: './config-accept.component.html',
  styleUrls: ['./config-accept.component.scss']
})
export class ConfigAcceptComponent implements OnInit {

  @Input() displayConfigWork: boolean = false;
  @Input() dataAssign: any;
  @Output() closeUserAssign  = new EventEmitter();

  value: string | undefined;

  selectedUsers: any[] = [];

  users: any[] = [
    { name: 'Nguyễn Văn A', key: 'A' },
    { name: 'Nguyễn Đức M', key: 'M' },
    { name: 'Phạm Như P', key: 'P' },
    { name: 'Nông Thị R', key: 'R' }
  ];

  constructor() { }

  ngOnInit() {
  }

  clearSelected(user){
    let users = [...this.selectedUsers];
    let index = users.findIndex(u => u.key === user.key);

    if (index !== -1) {
      users.splice(index, 1);
    }

    this.selectedUsers = users;

  }

  save(){
    this.closeUserAssign.emit(this.selectedUsers);
  }

}
