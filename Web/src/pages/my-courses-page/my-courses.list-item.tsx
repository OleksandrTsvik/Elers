import { Card, List } from 'antd';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { HidableBadgeRibbon } from '../../components';
import { MyCourseListItem } from '../../models/course.interface';

import courseImage from '../../assets/course.svg';

interface Props {
  item: MyCourseListItem;
}

export default function MyCoursesListItem({
  item: { id, title, imageUrl, isCreator },
}: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  return (
    <List.Item onClick={() => navigate(`/courses/${id}`)}>
      <HidableBadgeRibbon
        show={isCreator}
        text={t('my_courses_page.creator_badge')}
      >
        <Card
          hoverable
          bordered={false}
          cover={<img src={imageUrl || courseImage} alt={title} />}
        >
          <Card.Meta title={title} />
        </Card>
      </HidableBadgeRibbon>
    </List.Item>
  );
}
