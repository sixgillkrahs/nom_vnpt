import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-project-members',
  templateUrl: './project-members.component.html',
  styleUrls: ['./project-members.component.scss']
})
export class ProjectMembersComponent implements OnInit {

  members: any[] = [];
  teams: any[] = [];


  constructor() { }

  ngOnInit() {
    this.members = [
      {
        user: "Nguyễn Văn A",
        role: "View"
      },
      {
        user: "Nguyễn Văn B",
        role: "View"
      },
      {
        user: "Bùi Thị C",
        role: "Owner"
      },
     ];

     this.teams = [
      {
        team: "Dev",
        role: "View"
      },
      {
        team: "Check",
        role: "Owner"
      }
     ];
  }

}
