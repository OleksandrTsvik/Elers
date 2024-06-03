import { Skeleton } from 'antd';
import { useTranslation } from 'react-i18next';
import { useNavigate, useParams } from 'react-router-dom';

import { useUpdateCourseMaterialAssignmentMutation } from '../../api/course-materials.mutations.api';
import { useGetCourseMaterialAssignmentQuery } from '../../api/course-materials.queries.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';
import {
  CourseTabType,
  MaterialAssignmentForm,
  MaterialAssignmentFormValues,
  getCourseEditPagePath,
} from '../../shared';

interface Props {
  courseId: string;
  tabId: string;
  courseTabType: CourseTabType;
}

export default function MaterialAssignmentEditForm({
  courseId,
  tabId,
  courseTabType,
}: Props) {
  const { id } = useParams();

  const { t } = useTranslation();
  const navigate = useNavigate();

  const { data, isFetching, error } = useGetCourseMaterialAssignmentQuery({
    id,
  });

  const [updateCourseMaterial, { isLoading, error: errorUpdate }] =
    useUpdateCourseMaterialAssignmentMutation();

  const handleSubmit = async (values: MaterialAssignmentFormValues) => {
    await updateCourseMaterial({ id, ...values })
      .unwrap()
      .then(() =>
        navigate(getCourseEditPagePath(courseId, tabId, courseTabType)),
      );
  };

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
    <MaterialAssignmentForm
      initialValues={data}
      textOnSubmitButton={t('actions.save_changes')}
      isLoading={isLoading}
      error={errorUpdate}
      onSubmit={handleSubmit}
    />
  );
}
