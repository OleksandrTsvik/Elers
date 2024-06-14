import { HighlightOutlined, StarOutlined } from '@ant-design/icons';

import { GradeType } from '../../models/grade.interface';
import { CourseMaterialIcon, CourseMaterialType } from '../materials';

interface Props {
  type: GradeType;
}

export function GradeIcon({ type }: Props) {
  switch (type) {
    case GradeType.Assignment:
      return <CourseMaterialIcon type={CourseMaterialType.Assignment} />;
    case GradeType.Test:
      return <CourseMaterialIcon type={CourseMaterialType.Test} />;
    case GradeType.Manual:
      return <HighlightOutlined />;
    default:
      return <StarOutlined />;
  }
}
