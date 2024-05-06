import MaterialCreationContent from './material-creation.content';
import { CourseTab } from '../../../models/course.interface';

interface Props {
  tab: CourseTab;
}

export default function CourseEditTabContent({ tab }: Props) {
  return <MaterialCreationContent />;
}
