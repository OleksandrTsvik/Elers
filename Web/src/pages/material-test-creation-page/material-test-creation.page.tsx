import { Skeleton, Typography } from 'antd';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import MaterialTestCreationBreadcrumb from './material-test-creation.breadcrumb';
import MaterialTestCreationForm from './material-test-creation.form';
import MaterialTestCreationHead from './material-test-creation.head';
import { useGetCourseByTabIdQuery } from '../../api/courses.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function MaterialTestCreationPage() {
  const { tabId } = useParams();
  const { t } = useTranslation();

  const { data, isFetching, error } = useGetCourseByTabIdQuery({ id: tabId });

  if (isFetching) {
    return <Skeleton active />;
  }

  if (error) {
    return <NavigateToError error={error} />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <MaterialTestCreationHead />
      <MaterialTestCreationBreadcrumb
        courseId={data.courseId}
        courseTitle={data.title}
      />
      <MaterialTestCreationForm tabId={data.tabId} />
      <Typography.Paragraph italic strong>
        {t('material_test_creation_page.creation_form_note')}
      </Typography.Paragraph>
    </>
  );
}
