import { Image, List } from 'antd';
import { FaUsers } from 'react-icons/fa';
import { GiBookshelf } from 'react-icons/gi';
import { GrTest } from 'react-icons/gr';
import { Link } from 'react-router-dom';

import { IconText } from '../../components';
import { Course } from '../../models/course.interface';

import courseImage from '../../assets/course.svg';

interface Props {
  courseItem: Course;
}

export default function HomeListCourseItem({ courseItem }: Props) {
  return (
    <List.Item
      key={courseItem.title}
      actions={[
        <IconText icon={FaUsers} text="86" />,
        <IconText icon={GiBookshelf} text="34" />,
        <IconText icon={GrTest} text="5" />,
      ]}
      extra={
        <Image
          width={248}
          src={courseItem.photoUrl || courseImage}
          alt={courseItem.title}
        />
      }
    >
      <List.Item.Meta
        title={<Link to={`/courses/${courseItem.id}`}>{courseItem.title}</Link>}
        description={courseItem.description}
      />
    </List.Item>
  );
}
