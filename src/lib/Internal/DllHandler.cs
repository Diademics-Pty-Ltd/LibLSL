using System;
using System.Runtime.InteropServices;

namespace LSL.Internal
{
    internal class DllHandler
    {
        private const string libname = "lsl";

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_protocol_version();

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_library_version();

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern double lsl_local_clock();

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_create_streaminfo(string name, string type, int channel_count, double nominal_srate, ChannelFormatType channel_format, string source_id);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_destroy_streaminfo(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_get_name(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_get_type(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_get_channel_count(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern double lsl_get_nominal_srate(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern ChannelFormatType lsl_get_channel_format(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_get_source_id(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_get_version(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern double lsl_get_created_at(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_get_uid(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_get_session_id(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_get_hostname(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_get_desc(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_get_xml(IntPtr info);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_create_outlet(IntPtr info, int chunk_size, int max_buffered);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern void lsl_destroy_outlet(IntPtr obj);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_sample_ftp(IntPtr obj, float[] data, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_sample_dtp(IntPtr obj, double[] data, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_sample_itp(IntPtr obj, int[] data, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_sample_stp(IntPtr obj, short[] data, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_sample_ctp(IntPtr obj, char[] data, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_sample_strtp(IntPtr obj, string[] data, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_sample_buftp(IntPtr obj, char[][] data, uint[] lengths, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ftp(IntPtr obj, float[,] data, uint data_elements, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ftnp(IntPtr obj, float[,] data, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_dtp(IntPtr obj, double[,] data, uint data_elements, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_dtnp(IntPtr obj, double[,] data, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_itp(IntPtr obj, int[,] data, uint data_elements, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_itnp(IntPtr obj, int[,] data, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_stp(IntPtr obj, short[,] data, uint data_elements, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_stnp(IntPtr obj, short[,] data, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ctp(IntPtr obj, char[,] data, uint data_elements, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ctnp(IntPtr obj, char[,] data, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_strtp(IntPtr obj, string[,] data, uint data_elements, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_strtnp(IntPtr obj, string[,] data, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_buftp(IntPtr obj, char[][] data, uint[] lengths, uint data_elements, double timestamp, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_push_chunk_buftnp(IntPtr obj, char[][] data, uint[] lengths, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_have_consumers(IntPtr obj);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_wait_for_consumers(IntPtr obj, double timeout);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_get_info(IntPtr obj);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_resolve_all(IntPtr[] buffer, uint buffer_elements, double wait_time);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_resolve_byprop(IntPtr[] buffer, uint buffer_elements, string prop, string value, int minimum, double wait_time);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_resolve_bypred(IntPtr[] buffer, uint buffer_elements, string pred, int minimum, double wait_time);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_create_inlet(IntPtr info, int max_buflen, int max_chunklen, int recover);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern void lsl_destroy_inlet(IntPtr obj);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_get_fullinfo(IntPtr obj, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern void lsl_open_stream(IntPtr obj, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern void lsl_close_stream(IntPtr obj);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern double lsl_time_correction(IntPtr obj, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_set_postprocessing(IntPtr obj, PostProcessingOptions flags);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern double lsl_pull_sample_f(IntPtr obj, float[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern double lsl_pull_sample_d(IntPtr obj, double[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern double lsl_pull_sample_i(IntPtr obj, int[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern double lsl_pull_sample_s(IntPtr obj, short[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern double lsl_pull_sample_c(IntPtr obj, char[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern double lsl_pull_sample_str(IntPtr obj, IntPtr[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern double lsl_pull_sample_buf(IntPtr obj, char[][] buffer, uint[] buffer_lengths, int buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern void lsl_destroy_string(IntPtr str);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_f(IntPtr obj, float[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_d(IntPtr obj, double[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_i(IntPtr obj, int[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_s(IntPtr obj, short[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_c(IntPtr obj, char[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_str(IntPtr obj, IntPtr[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_buf(IntPtr obj, char[][,] data_buffer, uint[,] lengths_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern uint lsl_samples_available(IntPtr obj);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern uint lsl_was_clock_reset(IntPtr obj);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_first_child(IntPtr e);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_last_child(IntPtr e);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_next_sibling(IntPtr e);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_previous_sibling(IntPtr e);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_parent(IntPtr e);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_child(IntPtr e, string name);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_next_sibling_n(IntPtr e, string name);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_previous_sibling_n(IntPtr e, string name);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_empty(IntPtr e);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_is_text(IntPtr e);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_name(IntPtr e);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_value(IntPtr e);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_child_value(IntPtr e);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_child_value_n(IntPtr e, string name);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_append_child_value(IntPtr e, string name, string value);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_prepend_child_value(IntPtr e, string name, string value);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_set_child_value(IntPtr e, string name, string value);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_set_name(IntPtr e, string rhs);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_set_value(IntPtr e, string rhs);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_append_child(IntPtr e, string name);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_prepend_child(IntPtr e, string name);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_append_copy(IntPtr e, IntPtr e2);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_prepend_copy(IntPtr e, IntPtr e2);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern void lsl_remove_child_n(IntPtr e, string name);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern void lsl_remove_child(IntPtr e, IntPtr e2);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_create_continuous_resolver(double forget_after);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_create_continuous_resolver_byprop(string prop, string value, double forget_after);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr lsl_create_continuous_resolver_bypred(string pred, double forget_after);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern int lsl_resolver_results(IntPtr obj, IntPtr[] buffer, uint buffer_elements);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern void lsl_destroy_continuous_resolver(IntPtr obj);
    }
}
