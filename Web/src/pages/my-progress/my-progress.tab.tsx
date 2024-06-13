import { Typography } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import { MyGradesTable } from '../../shared';

interface Props {
  courseId: string;
}

export default function MyProgressTab({ courseId }: Props) {
  const { t } = useTranslation();

  return (
    <>
      <Typography.Paragraph>
        <Link to={`/courses/${courseId}`}>
          {t('my_progress_page.go_to_course')}
        </Link>
      </Typography.Paragraph>
      <MyGradesTable courseId={courseId} />
    </>
  );
}
