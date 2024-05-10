import { Flex, Radio, RadioChangeEvent, Spin, Typography } from 'antd';
import { useTranslation } from 'react-i18next';
import { useSearchParams } from 'react-router-dom';

import { useUpdateCourseTabTypeMutation } from '../../api/courses.api';
import { ErrorAlert } from '../../common';
import {
  CourseTabType,
  DEFAULT_COURSE_TAB_TYPE,
  SEARCH_PARAM_COURSE_TAB,
} from '../../shared';

import styles from './course-edit.module.scss';

interface Props {
  courseId: string;
  currentTabType?: string;
}

export default function CourseEditTabType({ courseId, currentTabType }: Props) {
  const [searchParams, setSearchParams] = useSearchParams();
  const { t } = useTranslation();

  const [updateCourseTabType, { isLoading, error }] =
    useUpdateCourseTabTypeMutation();

  const handleChange = async ({ target: { value } }: RadioChangeEvent) => {
    await updateCourseTabType({
      id: courseId,
      tabType: value as string,
    })
      .unwrap()
      .then(() => {
        if (searchParams.has(SEARCH_PARAM_COURSE_TAB)) {
          searchParams.delete(SEARCH_PARAM_COURSE_TAB);
          setSearchParams(searchParams);
        }
      });
  };

  return (
    <Spin spinning={isLoading}>
      <Flex className={styles.tabTypeContainer} vertical gap="small">
        {error && <ErrorAlert error={error} />}

        <Typography.Text>{t('course.tab_type')}:</Typography.Text>

        <Radio.Group
          optionType="button"
          buttonStyle="solid"
          disabled={isLoading}
          defaultValue={currentTabType ?? DEFAULT_COURSE_TAB_TYPE}
          options={[
            { label: t('course.tab_types.tabs'), value: CourseTabType.Tabs },
            {
              label: t('course.tab_types.sections'),
              value: CourseTabType.Sections,
            },
          ]}
          onChange={handleChange}
        />
      </Flex>
    </Spin>
  );
}
