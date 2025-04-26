import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-department-team-members',
  templateUrl: './department-team-members.component.html',
  styleUrls: ['./department-team-members.component.scss']
})
export class DepartMentTeamMembersComponent implements OnInit {

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
