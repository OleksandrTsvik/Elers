export interface CourseMember {
  id: string;
  userId: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  avatarUrl: string;
  courseRole?: {
    id?: string;
    description?: string;
  };
}
