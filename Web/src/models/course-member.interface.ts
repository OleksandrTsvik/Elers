export interface CourseMember {
  id: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  avatarUrl: string;
  courseRole?: {
    id?: string;
    description?: string;
  };
}
