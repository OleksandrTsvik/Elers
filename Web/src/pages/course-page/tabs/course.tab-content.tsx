import CourseTabMaterial from './course.tab-material';
import { CourseMaterial } from '../../../models/course-material.type';
import { CourseMaterialsEmpty } from '../../../shared';

interface Props {
  materials: CourseMaterial[];
}

export default function CourseTabContent({ materials }: Props) {
  if (!materials.length) {
    return <CourseMaterialsEmpty />;
  }

  return materials.map((item) => (
    <CourseTabMaterial key={item.id} material={item} />
  ));
}
