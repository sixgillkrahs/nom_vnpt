import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-project-works',
  templateUrl: './project-works.component.html',
  styleUrls: ['./project-works.component.scss']
})
export class ProjectWorksComponent implements OnInit {

  works: any[] = [];

  constructor() { }

  ngOnInit() {
    this.works = [
      {
        name: "Kế hoạch tuyển dụng dự án work",
        workStatus: true,
        deadline: "2023-11-21T09:58:25.649395",
        user: "Nguyễn Văn A",
        project: null
      },
      {
        name: "Xây dựng tài liệu yêu cầu hệ thống",
        workStatus: false,
        deadline: "2023-12-02T09:58:25.649395",
        user: "Nguyễn Văn A",
        project: null
      },
      {
        name: "Viết tài liệu kỹ thuật giải pháp",
        workStatus: null,
        deadline: "2023-12-18T09:58:25.649395",
        user: "Hoàng Ngọc Ánh",
        project: null
      },
      {
        name: "Vẽ Prototype",
        workStatus: null,
        deadline: "2023-11-30T09:58:25.649395",
        user: null,
        project: null
      },
     ];
  }

}
