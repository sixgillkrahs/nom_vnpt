import { CreateUpdateProjectRoleDto } from "@proxy/project-role/dtos";
import { CreateUpdateProjectStateDto } from "@proxy/project-state/dtos";
// import { CreateUpdateResourseManagementDto } from "@proxy/resourse-management/dtos";

export class ProjectDetailData {
  id?: any;
  header: string;
  active?: any;
}

export class ProjectRoleData {
  id?: any;
  header: string;
  param?: CreateUpdateProjectRoleDto;
}

export class ProjectStateData {
  id?: any;
  header: string;
  param?: CreateUpdateProjectStateDto;
}

// export class DocumentData{
//   id?: any;
//   header: string;
//   param?: CreateUpdateResourseManagementDto;
// }


