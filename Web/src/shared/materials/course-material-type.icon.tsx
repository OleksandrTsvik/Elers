import Icon, {
  LinkOutlined,
  SolutionOutlined,
  ExperimentOutlined,
} from '@ant-design/icons';
import { GetProps } from 'antd';
import { FaBox } from 'react-icons/fa';
import { IoTextSharp } from 'react-icons/io5';

import { CourseMaterialFileIcon } from './course-material-file.icon';
import { CourseMaterialType } from './course-material.enum';

type IconProps = Omit<GetProps<typeof Icon>, 'component'>;

interface Props extends IconProps {
  type: CourseMaterialType;
}

export function CourseMaterialIcon({ type, ...props }: Props) {
  switch (type) {
    case CourseMaterialType.Content:
      return <Icon component={IoTextSharp} {...props} />;
    case CourseMaterialType.Assignment:
      return <SolutionOutlined {...props} />;
    case CourseMaterialType.File:
      return <CourseMaterialFileIcon {...props} />;
    case CourseMaterialType.Test:
      return <ExperimentOutlined {...props} />;
    case CourseMaterialType.Link:
      return <LinkOutlined {...props} />;
    default:
      return <Icon component={FaBox} {...props} />;
  }
}
