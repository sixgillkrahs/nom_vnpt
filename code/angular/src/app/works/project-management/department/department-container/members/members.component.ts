import { Component, OnInit } from '@angular/core';

interface PageEvent {
  first: number;
  rows: number;
  page: number;
  pageCount: number;
}

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.scss']
})
export class MembersComponent implements OnInit {

  totalRecords: number;
  rows: number = 4;
  currentPage: number = 1;
  first: number = 0;
  pageLinkSize: number = 5;

  members: any[] = [];
  searchResult: any[] = [];
  keyS: boolean = false;

  searchValue: any;

  constructor() { }

  get paginatorMember() {
    return this.members.slice((this.currentPage-1) * this.rows, this.currentPage * this.rows)
  }
  get paginatorSearchResult() {
    return this.searchResult.slice((this.currentPage-1) * this.rows, this.currentPage * this.rows)
  }

  ngOnInit() {
    this.members = [
      {
        id: "reeerer-34eere-45rtr-534teyh",
        username: "Username",
        fullname: "Nguyễn Văn A",
        avatar: null,
        oranization: "Csoft1",
        position: "Chuyên viên lập trình",
        team: ["Lead","Dev"]
      },
      {
        id: "hahaer-34eere-45rtr-5334eteyh",
        username: "Username",
        fullname: "Nguyễn Văn A",
        avatar: null,
        oranization: "Csoft1",
        position: "Chuyên viên lập trình",
        team: ["Lead","Dev"]
      },
      {
        id: "fg455-34eere-45rtr-455f",
        username: "Username",
        fullname: "Nguyễn Văn A",
        avatar: null,
        oranization: "Csoft",
        position: "Chuyên viên lập trình",
        team: ["Lead","Dev"]
      },
      {
        id: "3334sfg-34eere-45gj-4s",
        username: "Username",
        fullname: "Nguyễn Văn A",
        avatar: null,
        oranization: "Csoft",
        position: "Chuyên viên lập trình",
        team: ["Lead","Dev"]
      },
      {
        id: "ewes34-45rgd-dfsf34-ghjdfg3",
        username: "Username",
        fullname: "Nguyễn Văn A",
        avatar: null,
        oranization: "Csoft",
        position: "Chuyên viên lập trình",
        team: ["Lead","Dev"]
      },
      {
        id: "ewes34-45rgd-dfsf34-ghjdfg3",
        username: "Username",
        fullname: "Nguyễn Văn A",
        avatar: null,
        oranization: "Csoft",
        position: "Chuyên viên lập trình",
        team: ["Lead","Dev"]
      },
      {
        id: "ewes34-45rgd-dfsf34-ghjdfg3",
        username: "Username",
        fullname: "Nguyễn Văn A",
        avatar: null,
        oranization: "Csoft",
        position: "Chuyên viên lập trình",
        team: ["Lead","Dev"]
      },
      {
        id: "ewes34-45rgd-dfsf34-ghjdfg3",
        username: "Username",
        fullname: "Nguyễn Văn E",
        avatar: null,
        oranization: "Csoft",
        position: "Chuyên viên lập trình",
        team: ["Lead","Dev"]
      },
      {
        id: "ewes34-45rgd-dfsf34-ghjdfg3",
        username: "Username",
        fullname: "Nguyễn Văn AV",
        avatar: null,
        oranization: "Csoft",
        position: "Chuyên viên lập trình",
        team: ["Lead1","Dev"]
      },
      {
        id: "ewes34-45rgd-dfsf34-ghjdfg3",
        username: "Username",
        fullname: "Nguyễn Văn A",
        avatar: null,
        oranization: "Csoft",
        position: "Chuyên viên lập trình",
        team: ["Lead1","Dev"]
      },
      {
        id: "ewes34-45rgd-dfsf34-ghjdfg3",
        username: "Username",
        fullname: "Nguyễn Văn C",
        avatar: null,
        oranization: "Csoft",
        position: "Chuyên viên lập trình",
        team: ["Lead","Dev"]
      },
      {
        id: "ewes34-45rgd-dfsf34-ghjdfg3",
        username: "Username",
        fullname: "Nguyễn Văn D",
        avatar: null,
        oranization: "Csoft",
        position: "Chuyên viên lập trình",
        team: ["Lead","Dev"]
      }
    ]
    this.totalRecords = this.members.length;
  }

  onPageChange(event: PageEvent) {
    this.first = event.first;
    this.currentPage = event.page+1;
  }

  search() {
    let text = this.searchValue.toLowerCase();
    if(text === ""){
      this.keyS = false;
      this.totalRecords = this.members.length;
    }else{
      this.keyS = true;
      this.totalRecords = this.searchResult.length;
    }

    this.searchResult = this.members.filter(m =>
      (m.username.toLowerCase()).includes(text) ||
      (m.fullname.toLowerCase()).includes(text) ||
      (m.oranization.toLowerCase()).includes(text) ||
      (m.position.toLowerCase()).includes(text) ||
      (m.team.toString().toLowerCase()).includes(text)
    )

  }

}
