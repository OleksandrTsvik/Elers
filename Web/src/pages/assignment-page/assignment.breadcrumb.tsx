import { CourseBreadcrumb } from '../../shared';

interface Props {
  courseId?: string;
  title: string;
}

export default function AssignmentBreadcrumb({ courseId, title }: Props) {
  return <CourseBreadcrumb courseId={courseId} title={title} />;
}
