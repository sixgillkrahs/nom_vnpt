import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.scss']
})
export class TeamsComponent implements OnInit {

  constructor() { }

  owners: any[] = [
    {
      owner: "Owner",
      project: "Project",
      members: 5
    },
    {
      owner: "Owner",
      project: "Project",
      members: 5
    },
    {
      owner: "Owner",
      project: "Project",
      members: 5
    },
    {
      owner: "Owner",
      project: "Project",
      members: 5
    },
    {
      owner: "Owner",
      project: "Project",
      members: 5
    }
  ]

  ngOnInit() {
    this.generateRandomImg();
  }

  generateRandomImg() {

    const name = ["mau0tim.jpg", "nen-go.jpg", "ocean.jpg", "vang.jpg", "vi-sinh-vat.jpg"]

    for (const [index,items] of this.owners.entries()) {
      const imageIndex = index % 5;
      items.image = `assets/images/bgr-image/${name[imageIndex]}`;
    }
  }

}
