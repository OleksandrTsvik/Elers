import { SolutionOutlined } from '@ant-design/icons';
import { Image, List } from 'antd';
import { FaUsers } from 'react-icons/fa';
import { GiBookshelf } from 'react-icons/gi';
import { GrTest } from 'react-icons/gr';
import { Link } from 'react-router-dom';

import { IconText } from '../../components';
import { CourseListItem } from '../../models/course.interface';

import courseImage from '../../assets/course.svg';

import styles from './home.module.scss';

interface Props {
  courseItem: CourseListItem;
}

export default function HomeCourseListItem({ courseItem }: Props) {
  return (
    <List.Item
      key={courseItem.title}
      actions={[
        <IconText icon={FaUsers} text={courseItem.countMembers} />,
        <IconText icon={GiBookshelf} text={courseItem.countMaterials} />,
        <IconText icon={SolutionOutlined} text={courseItem.countAssignments} />,
        <IconText icon={GrTest} text={courseItem.countTests} />,
      ]}
      extra={
        <Image
          width={248}
          src={courseItem.imageUrl || courseImage}
          alt={courseItem.title}
        />
      }
    >
      <List.Item.Meta
        className={styles.courseItemMeta}
        title={<Link to={`/courses/${courseItem.id}`}>{courseItem.title}</Link>}
        description={courseItem.description}
      />
    </List.Item>
  );
}
